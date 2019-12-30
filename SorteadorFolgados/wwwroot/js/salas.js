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
        type: "PUT",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err) }
    })
}

function criarSala(sala) {
    const url = '/api/sala/';
    const data = sala;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err) }
    })
}

function atualizarSala(sala) {
    const url = '/api/sala/' + sala.salaId;
    const data = sala;
    $.ajax({
        type: "PUT",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err) }
    })
}

function deletarSala(sala) {
    const url = '/api/sala/' + sala.salaId;
    const data = sala;
    $.ajax({
        type: "DELETE",
        url: url,
        data: data,
        contentType: "application/json",
        success: (res) => { console.log(res) },
        error: (err) => { console.error(err) }
    })
}