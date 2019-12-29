function iniciarSorteio(salaId) {
    const url = '/api/sorteios/iniciar-sorteio';
    const data = salaId;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err)}
    })
}

function encerrarSorteio() {
    const url = '/api/sorteios/encerrar-sorteio';
    const data = {};
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err) }
    })
}