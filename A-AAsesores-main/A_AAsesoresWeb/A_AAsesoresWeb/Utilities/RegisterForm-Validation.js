$(document).ready(function () {

    campoCedula = document.getElementById('UserEnt_Identification');
    campoCedula.addEventListener('input', function () {
        var valor = campoCedula.value.trim();
        if (valor.length > 0 && valor[0] !== '0') {
            valor = '0' + valor;
        }
        campoCedula.value = valor;
    });
});
