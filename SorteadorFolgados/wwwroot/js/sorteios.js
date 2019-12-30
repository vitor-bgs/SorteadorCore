"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sorteio").build();
connection.on("atualizarSorteio", atualizarSorteio);
connection.on("sortearOk", sortearOk);
connection.start()
    .then(function () {
        console.log("conexão iniciada.");
    })
    .catch(function (err) {
        console.error(err.toString());
        SnackbarMessage("Erro ao conectar");
    });

function sortearOk(participacao) {
    SnackbarMessage(participacao.participante.nome + ": " + participacao.pontos);
}

function atualizarSorteio(sorteioAtual) {
    const sala = sorteioAtual.sala.nome;
    const dataSorteio = new Date(sorteioAtual.dataInicio).toLocaleString('pt-BR');
    $("[id='nomeSorteio']").text(`${sala}`);
    $("[id='datasSorteio']").text(`Início: ${dataSorteio}`);

    sorteioAtual.participacoes.sort((a, b) => b.pontos - a.pontos);
    $("table[id='Participacoes'] tbody").empty().append(
        sorteioAtual.participacoes.map((participacao, i) => {
            const indiceUltimoParticipante = sorteioAtual.participacoes.length - 1;
            const vencedorMaioresPontos = (i < sorteioAtual.sala.quantidadeVencedoresMaioresPontos);
            const vencedorMenoresPontos = ((indiceUltimoParticipante - i) < sorteioAtual.sala.quantidadeVencedoresMenoresPontos);
            const vencedor = (vencedorMaioresPontos || vencedorMenoresPontos);

            return `<tr class="mdc-data-table__row ${vencedor ? 'mdc-data-table__row--selected' : ''}">
                <td class="mdc-data-table__cell mdc-data-table__cell--numeric">${participacao.pontos}</td>
                <td class="mdc-data-table__cell">${participacao.participante.nome}</td>
                <td class="mdc-data-table__cell">${participacao.enderecoIP}</td>
                <td class="mdc-data-table__cell">${new Date(participacao.dataParticipacao).toLocaleString('pt-BR')}</td>
            </tr>`
        }
        )
    );
}



function sortear(nomeParticipante, ) {
    connection.invoke("Sortear", nomeParticipante).catch(function (err) { console.error(err); });
}

function sortearClick(){
    const nomeParticipante = $("input[id='nomeParticipante']").val();
    sortear(nomeParticipante);
    event.preventDefault();
}