"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sorteio").build();
connection.on("atualizarSorteio", atualizarSorteio);
connection.start()
    .then(function () { console.log("conexão iniciada."); })
    .catch(function (err) { console.error(err.toString()); });

function atualizarSorteio(sorteioAtual) {
    console.log(sorteioAtual);
    const sala = sorteioAtual.sala.nome;
    const dataSorteio = new Date(sorteioAtual.dataInicio).toLocaleString('pt-BR');
    $("h2[id='Sorteio']").text(`${sala} (${dataSorteio})`);

    sorteioAtual.participacoes.sort((a, b) => b.pontos - a.pontos);
    $("table[id='Participacoes'] tbody").empty().append(
        sorteioAtual.participacoes.map((participacao) => 
            `<tr>
                <td>${participacao.pontos}</td>
                <td>${participacao.participante.nome}</td>
                <td>${participacao.enderecoIP}</td>
                <td>${new Date(participacao.dataParticipacao).toLocaleString('pt-BR')}</td>
            </tr>`
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