$(document).ready(function () {
    $("#employeeForm").submit(function () {
        clearErrorMessages();
        var isValid = true;
        isValid = validateField("#UserEnt_Identification", "Por favor, ingrese el número de identificación.") && isValid;
        isValid = validateField("#UserEnt_Email,#Email", "Por favor, ingrese una dirección de correo electrónico.") && isValid;
        isValid = validateField("#UserEnt_PhoneNumber,#PhoneNumber", "Por favor, ingrese el número de teléfono.") && isValid;
        isValid = validateField("#QuoteDetailsEnt_WalkInClosetQuantity", "Por favor, ingrese la cantidad de WalkInClosets.") && isValid;
        // Puedes agregar más validaciones aquí según sea necesario...
        return isValid;
    });
    
        function validateField(selector, errorMessage) {
            var field = $(selector);
            if (field.val().trim() === '') {
                displayErrorMessage(field.attr('id') + '-error', errorMessage);
                return false;
            }
            return true;
        }

        function displayErrorMessage(elementId, message) {
            $("#" + elementId).text(message).css({ 'visibility': 'visible', 'overflow': 'visible' });
        }

        function clearErrorMessages() {
            $(".text-danger").css({ 'visibility': 'hidden', 'overflow': 'hidden' });
        }
    });

    $(document).ready(function () {
        // Vincula la función agregarGuion al evento keyup del campo de entrada con id "miCampo"
        $("#UserEnt_PhoneNumber,#PhoneNumber").on("keyup", function () {
            agregarGuion(this);
        });
    });

    //La función agrega un guión automáticamente al teléfono en el medio
    function agregarGuion(texto) {
        //Extrae la variable desde el elemento que se obtiene por su id
        let phone = document.getElementById(texto.id);
        //Si se ingresa un - esta linea lo retira y lo remplaza con un ""
        phone = phone.value.split('-').join('');
        //Por medio de un regex se separan los numeros en grupos de 4 y se unen mediante un -
        let telefonoActualizado = phone.match(/[0-9]{1,4}/g).join('-');
        //Actualiza el valor al que se define con el -
        document.getElementById(texto.id).value = telefonoActualizado;
    }

    //Esta función previene que el usuario digite letras mediante su código ASCII
    function NoDigitarLetras(evt) {
        //Extrae el código de lo que el usuario digita
        var charCode = (evt.which) ? evt.which : evt.keyCode
        //Los números están comprendidos desde el 48 hasta el 57 en ASCII
        if (charCode < 48 || charCode > 57)
            //Si no es parte del rango evita que se escriba
            return false;
        //Si es valido permite la escritura
        return true;
    }

    document.addEventListener('DOMContentLoaded', function () {
        // Obtén el elemento del campo de cédula por su ID
        var campoCedula = document.getElementById('UserEnt_Identification');

        // Agrega un listener para el evento input
        campoCedula.addEventListener('input', function () {
            // Obtiene el valor actual del campo
            var valor = campoCedula.value.trim();

            // Verifica si el valor comienza con 0, y si no, agrega un 0 al inicio
            if (valor.length > 0 && valor[0] !== '0') {
                valor = '0' + valor;
            }

            // Formatea el valor con guiones
            valor = valor.replace(/[^0-9]/g, ''); // Elimina caracteres no numéricos
            valor = valor.replace(/(\d{2})(\d{4})(\d{4})/, '$1-$2-$3');

            // Actualiza el valor del campo con el formato deseado
            campoCedula.value = valor;
        });
    });
