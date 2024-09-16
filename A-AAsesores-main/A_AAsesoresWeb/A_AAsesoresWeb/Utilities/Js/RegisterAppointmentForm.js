///* Máscaras para los campos de entrada*/
/*$('#PhoneNumber').mask('0000-0000');*/
//// Validación del formulario con Parsley
/*$('#RegisterAppointmentForm').parsley();*/
//Coloca Mayusculas la primera letra de cada palabra
//document.ready = function () {
//    campoCedula = document.getElementById('Identification');
//    campoCedula.addEventListener('input', function () {
//        var valor = campoCedula.value.trim();
//        if (valor.length > 0 && valor[0] !== '0') {
//            valor = '0' + valor;
//        }
//        campoCedula.value = valor;
//    });
//};
function Capitalize(word) {
    return word
        .split('')
        .map((letter, index) =>
            index ? letter.toLowerCase() : letter.toUpperCase(),
        )
        .join('');
}

////Consulta un API para obtener el nombre de la persona
function GetUserDataByAPI() {
    let identificacion = $("#Identification").val().split("-").join("");
    if (identificacion.length > 0) {
        $.ajax({
            url: 'https://apis.gometa.org/cedulas/' + identificacion,
            type: "GET",
            dataType: "json",
            success: function (data) {
                // Comprueba si la respuesta tiene resultados
                if (data.results && data.results.length > 0) {
                    $("#Name").val(Capitalize(data.results[0].firstname1) + " " + Capitalize(data.results[0].firstname2)).prop('readonly', true);
                    $("#FirstLastName").val(Capitalize(data.results[0].lastname1)).prop('readonly', true);
                    $("#SecondLastName").val(Capitalize(data.results[0].lastname2)).prop('readonly', true);
                }
                else {
                    // Si no hay resultados en la API local, habilitar los campos y dejarlos en blanco
                    $("#Name").val("").prop('readonly', false);
                    $("#FirstLastName").val("").prop('readonly', false);
                    $("#SecondLastName").val("").prop('readonly', false);
                    $("#Email").val("");
                    $("#PhoneNumber").val("");
                    $("#Id").val(0);
                }
            }
        });
    }
}


function ConsultUserData() {
    let identificacion = $("#Identification").val().split("-").join("");
    if (identificacion.length !== 9) {
        $.ajax({
            url: '/Users/GetUserByIdentification?q=' + identificacion,
            type: "GET",
            datatype: "json",
            success: function (data) {
                // Comprueba si la respuesta tiene resultados
                $("#Name").val(data.Name).prop('readonly', true);
                $("#FirstLastName").val(data.FirstLastName).prop('readonly', true);
                $("#SecondLastName").val(data.SecondLastName).prop('readonly', true);
                $("#Email").val(data.Email);
                $("#PhoneNumber").val(data.PhoneNumber);
                $("#UserId").val(data.Id);
            },
            error: function (error) {
                // Manejar el error si es necesario
                GetUserDataByAPI();
            }
        });
    }
}

// Restablece los Campos del Usuario
function ResetUserFields() {
    $("#Name").val("").removeAttr('readonly');
    $("#FirstLastName").val("").removeAttr('readonly');
    $("#SecondLastName").val("").removeAttr('readonly');
    $("#Email").val("");
    $("#PhoneNumber").val("");
    $("#UserId").val(0);
}
