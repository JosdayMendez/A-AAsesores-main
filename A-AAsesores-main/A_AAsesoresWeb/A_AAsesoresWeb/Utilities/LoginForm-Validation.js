$(document).ready(function () {
    $('#Identification').mask('00-0000-0000-0000');
    campoCedula = document.getElementById('Identification');
    campoCedula.addEventListener('input', function () {

        var valor = campoCedula.value.trim();

        if (valor.length > 0 && valor[0] !== '0') {
            valor = '0' + valor;
        }
        campoCedula.value = valor;
    });
});