function iniciarSorteio(salaId) {
    const url = '/api/sorteios/iniciar-sorteio/' + salaId;
    const data = salaId;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => {
            console.log(res);
            obterSalas(preencherSalas);
            aviso("O sorteio foi iniciado");
        },
        error: (err) => { console.error(err)}
    })
}

function encerrarSorteio() {
    const url = '/api/sorteios/encerrar-sorteio';
    const data = {};
    $.ajax({
        type: "PUT",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => {
            console.log(res);
            obterSalas(preencherSalas);
            aviso("Sorteio encerrado");
        },
        error: (err) => { console.error(err) }
    })
}

function criarSala(sala) {
    const url = '/api/salas/';
    const data = sala;
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json",
        success: (res) => {
            console.log(res);
            aviso("Sala criada");
        },
        error: (err) => {
            console.error(err);
            aviso("Erro ao criar sala");
        }
    })
}

function atualizarSala(sala) {
    const url = '/api/salas/' + sala.SalaId;
    const data = sala;
    $.ajax({
        type: "PUT",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: 'json',
        success: (res) => {
            console.log(res);
            aviso("As alterações foram salvas");
        },
        error: (err) => {
            console.error(err);
            aviso("Erro ao salvar as alterações")
        }
    })
}

function deletarSala(sala) {
    const url = '/api/salas/' + sala.SalaId;
    const data = sala;
    $.ajax({
        type: "DELETE",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json",
        success: (res) => {
            console.log(res);
            aviso("Sala deletada");
        },
        error: (err) => {
            console.error(err);
            aviso("Erro ao deletar a sala")
        }
    })
}

function obterSalas(callback) {
    const url = '/api/salas/';
    const data = {};
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { callback(res) },
        error: (err) => {
            console.error(err);
            aviso("Erro ao carregar as salas");
        }
    })
}

function preencherSalas(listaSalas) {
    $('table[id="salas"] tbody').empty();
    listaSalas.map((item, i) => {
        const action = (item.estaNoSorteioAtual) ?
            `<a href="#" onclick="encerrarSorteio()">Encerrar Sorteio</a>`
            :
            `<a href="#" onclick="iniciarSorteio(${item.salaId})">Iniciar Sorteio</a>`
  
        $('table[id="salas"] tbody').append(
            `<tr class="mdc-data-table__row">
                <td class="mdc-data-table__cell--numeric">${item.salaId}</td>
                <td class="mdc-data-table__cell">${item.nome}</td>
                <td class="mdc-data-table__cell">${item.quantidadeVencedoresMaioresPontos}</td>
                <td class="mdc-data-table__cell">${item.quantidadeVencedoresMenoresPontos}</td>
                <td class="mdc-data-table__cell">
                    ${action} |
                    <a href="Salas/Edit/${item.salaId}">Editar</a> |
                    <a href="Salas/Delete/${item.salaId}">Deletar</a>
                </td>
            </tr>`
        )
    });
}

function editarSalaClick() {
    const sala = obterSalaParaEditar();
    atualizarSala(sala);
    event.preventDefault();
}

function deletarSalaClick() {
    const sala = obterSalaParaDeletar();
    deletarSala(sala);
    event.preventDefault();
}

function criarSalaClick() {
    const sala = obterSalaParaCriar();
    criarSala(sala);
    event.preventDefault();
}

function obterSalaParaCriar() {
    return {
        "Nome": $("[id='nome']").val(),
        "QuantidadeVencedoresMaioresPontos": parseInt($("[id='quantidadeVencedoresMaioresPontos']").val()),
        "QuantidadeVencedoresMenoresPontos": parseInt($("[id='quantidadeVencedoresMenoresPontos']").val())
    }
}

function obterSalaParaEditar() {
    return {
        "SalaId": parseInt($("[id='salaId']").val()),
        "Nome": $("[id='nome']").val(),
        "QuantidadeVencedoresMaioresPontos": parseInt($("[id='quantidadeVencedoresMaioresPontos']").val()),
        "QuantidadeVencedoresMenoresPontos": parseInt($("[id='quantidadeVencedoresMenoresPontos']").val())
    }
}

function obterSalaParaDeletar() {
    return {
        "SalaId": parseInt($("[id='salaId']").text()),
        "Nome": $("[id='nome']").text(),
        "QuantidadeVencedoresMaioresPontos": parseInt($("[id='quantidadeVencedoresMaioresPontos']").text()),
        "QuantidadeVencedoresMenoresPontos": parseInt($("[id='quantidadeVencedoresMenoresPontos']").text())
    }
}