"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sorteio").build();
connection.on("atualizarSorteio", atualizarSorteio);
connection.on("atualizarVencedores", obterVencedores);
connection.on("aviso", aviso);
connection.start()
    .then(function () {
        console.log("conexão iniciada.");
    })
    .catch(function (err) {
        console.error(err.toString());
        SnackbarMessage("Erro ao conectar");
    });

function atualizarSorteio(sorteioAtual) {
    if (sorteioAtual == null) {

        $("[id='nomeSorteio']").text(`Não há sorteio ativo`);
        $("[id='datasSorteio']").text(``);
        $("table[id='Participacoes'] tbody").empty()
        return;
    }
    const sala = sorteioAtual.sala.nome;
    const dataSorteio = new Date(sorteioAtual.dataInicio).toLocaleString('pt-BR');
    $("[id='nomeSorteio']").text(`${sala}`);
    $("[id='datasSorteio']").text(`Início: ${dataSorteio}`);

    sorteioAtual.participacoes.sort((a, b) => b.pontos - a.pontos);

    $(`[id="vencedoresSemana0"] .mdc-chip--selected`).remove();
    $("table[id='Participacoes'] tbody").empty().append(
        sorteioAtual.participacoes.filter((p) => p.participacaoValida).map((participacao, i) => {
            const indiceUltimoParticipante = sorteioAtual.participacoes.filter((p)=> p.participacaoValida).length - 1;
            const vencedorMaioresPontos = (i < sorteioAtual.sala.quantidadeVencedoresMaioresPontos);
            const vencedorMenoresPontos = ((indiceUltimoParticipante - i) < sorteioAtual.sala.quantidadeVencedoresMenoresPontos);
            const vencedor = (vencedorMaioresPontos || vencedorMenoresPontos);
            if (vencedor) {
                atualizarVencedorSorteioAtual(sorteioAtual.sala.nome, participacao.participante.nome + " " + participacao.pontos)
            }
            return `<tr class="mdc-data-table__row ${vencedor ? 'mdc-data-table__row--selected' : ''}">
                <td class="mdc-data-table__cell mdc-data-table__cell--numeric">${participacao.pontos}</td>
                <td class="mdc-data-table__cell">${participacao.participante.nome}</td>
                <td class="mdc-data-table__cell">${participacao.enderecoIP.substring(0, participacao.enderecoIP.lastIndexOf("."))}</td>
                <td class="mdc-data-table__cell">${new Date(participacao.dataParticipacao).toLocaleString('pt-BR')}</td>
                ${userAuthenticated ? `<td class="mdc-data-table__cell"> <a href="#" onclick="marcarParticipacaoInvalida(${participacao.sorteioDetalheId})">Invalidar Participação</a></td>` : `` }
                ${!userAuthenticated ? `<td class="mdc-data-table__cell">Válida</td>` : ``}
            </tr>`
        })
    )
        .append(
            sorteioAtual.participacoes.filter((p) => !p.participacaoValida).map((participacao, i) => {
                return `<tr class="mdc-data-table__row mdc-data-table__row--disabled">
                            <td class="mdc-data-table__cell mdc-data-table__cell--numeric">${participacao.pontos}</td>
                            <td class="mdc-data-table__cell">${participacao.participante.nome}</td>
                            <td class="mdc-data-table__cell">${participacao.enderecoIP.substring(0, participacao.enderecoIP.lastIndexOf("."))}</td>
                            <td class="mdc-data-table__cell">${new Date(participacao.dataParticipacao).toLocaleString('pt-BR')}</td>
                            ${userAuthenticated ? `<td class="mdc-data-table__cell"> <a href="#" onclick="marcarParticipacaoValida(${participacao.sorteioDetalheId})">Reverter Invalidação</a></td>` : `` }
                            ${!userAuthenticated ? `<td class="mdc-data-table__cell">Inválida</td>` : ``}
                        </tr>`
            })
        )
}

function adicionarVencedor(idSemana, nomeSala, nomeVencedor) {
    $(`[id="${idSemana}"`).append(
        `<div class="mdc-chip" role="row">
            <div class="mdc-chip__ripple"></div>
            <span role="gridcell">
                <span role="button" class="mdc-chip__text">[${nomeSala}] ${nomeVencedor}</span>
            </span>
        </div>`
    );
}

function atualizarVencedorSorteioAtual(nomeSala, nomeVencedor) {
    $(`[id="vencedoresSemana0"]`).prepend(
        `<div class="mdc-chip mdc-chip--selected" role="row">
            <div class="mdc-chip__ripple"></div>
            <span role="gridcell">
                <span role="button" tabindex="0" class="mdc-chip__text">[${nomeSala}] ${nomeVencedor}</span>
            </span>
        </div>`
    );
}

function sortear(nomeParticipante) {
    if (nomeParticipante.trim() == '') {
        aviso("Nickname não pode estar vazio");
        return;
    }
    connection.invoke("Sortear", nomeParticipante).catch(function (err) { console.error(err); });
}

function sortearClick(e) {
    if (e != undefined) {
        var key = e.which || e.keyCode;
        if (key != 13) {
            return;
        }
    }
    const nomeParticipante = $("input[id='nomeParticipante']").val();
    sortear(nomeParticipante);
    event.preventDefault();
}

function obterVencedores() {
    [...Array(5).keys()].map(i => obterVencedoresSemana(i));
}

function obterVencedoresSemana(nSemana) {
    const url = '/api/Sorteios/obter-vencedores-sorteios/' + nSemana;
    $.ajax({
        type: "GET",
        url: url,
        data: {},
        contentType: "application/json",
        success: (res) => {
            $('[id="vencedoresSemana' + nSemana + '"]').empty();
            res.map(sorteio => {
                if (sorteio.ativo) {
                    return;
                }
                sorteio.participacoes.map(p => {
                    adicionarVencedor('vencedoresSemana' + nSemana, sorteio.sala.nome, p.participante.nome)
                })
            });
        },
        error: (err) => { console.error(err) }
    })
}

function marcarParticipacaoInvalida(sorteioDetalheId) {
    const url = '/api/sorteios/marcar-participacao-invalida/' + sorteioDetalheId;
    $.ajax({
        type: "PUT",
        url: url,
        data: {},
        contentType: "application/json",
        success: (res) => {
            console.log("Participação " + sorteioDetalheId + " invalidada")
        },
        error: (err) => { console.error(err) }
    })
}

function marcarParticipacaoValida(sorteioDetalheId) {
    const url = '/api/sorteios/marcar-participacao-valida/' + sorteioDetalheId;
    $.ajax({
        type: "PUT",
        url: url,
        data: {},
        contentType: "application/json",
        success: (res) => {
            console.log("Participação " + sorteioDetalheId + " marcada como válida")
        },
        error: (err) => { console.error(err) }
    })
}