//Coloca Mayusculas la primera letra de cada palabra
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
    let identificacion = $("#UserEnt_Identification").val().split("-").join("");
    if (identificacion.length > 0) {
        $.ajax({
            url: 'https://apis.gometa.org/cedulas/' + identificacion,
            type: "GET",
            dataType: "json",
            success: function (data) {
                // Comprueba si la respuesta tiene resultados
                if (data.results && data.results.length > 0) {
                    $("#UserEnt_Name").val(Capitalize(data.results[0].firstname1) + " " + Capitalize(data.results[0].firstname2)).prop('readonly', true);
                    $("#UserEnt_FirstLastName").val(Capitalize(data.results[0].lastname1)).prop('readonly', true);
                    $("#UserEnt_SecondLastName").val(Capitalize(data.results[0].lastname2)).prop('readonly', true);
                }
                else {
                    // Si no hay resultados en la API local, habilitar los campos y dejarlos en blanco
                    $("#UserEnt_Name").val("").prop('readonly', false);
                    $("#UserEnt_FirstLastName").val("").prop('readonly', false);
                    $("#UserEnt_SecondLastName").val("").prop('readonly', false);
                    $("#UserEnt_Email").val("");
                    $("#UserEnt_PhoneNumber").val("");
                    $("#UserEnt_Id").val(0);
                }
            }
        });
    }
}


function ConsultUserData() {
    let identificacion = $("#UserEnt_Identification").val().split("-").join("");
    if (identificacion.length !== 9) {
        $.ajax({
            url: '/Users/GetUserByIdentification?q=' + identificacion,
            type: "GET",
            datatype: "json",
            success: function (data) {
                // Comprueba si la respuesta tiene resultados
                $("#UserEnt_Name").val(data.Name).prop('readonly', true);
                $("#UserEnt_FirstLastName").val(data.FirstLastName).prop('readonly', true);
                $("#UserEnt_SecondLastName").val(data.SecondLastName).prop('readonly', true);
                $("#UserEnt_Email").val(data.Email);
                $("#UserEnt_PhoneNumber").val(data.PhoneNumber);
                $("#UserEnt_Id").val(data.Id);
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
    $("#UserEnt_Name").val("").removeAttr('readonly');
    $("#UserEnt_FirstLastName").val("").removeAttr('readonly');
    $("#UserEnt_SecondLastName").val("").removeAttr('readonly');
    $("#UserEnt_Email").val("");
    $("#UserEnt_PhoneNumber").val("");
    $("#UserEnt_Id").val(0);
}

