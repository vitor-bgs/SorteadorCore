// Write your Javascript code.
function SnackbarMessage(texto) {
    const snackbar = new mdc.snackbar.MDCSnackbar(document.querySelector('.mdc-snackbar'));
    snackbar.labelText = texto;
    snackbar.open();
}