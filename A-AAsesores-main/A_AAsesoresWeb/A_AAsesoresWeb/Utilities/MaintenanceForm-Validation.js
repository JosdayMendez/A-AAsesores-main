$(document).ready(function () {
    $('#MaintenanceForm').parsley();

    $("#MaintenanceForm").submit(function (event) {
        event.preventDefault();
        clearErrorMessages();
        var isValid = true;
        isValid = validateField("#Maintenance_Name", "Por favor, ingrese el nombre.") && isValid;
        
         isValid = validateField("#Maintenance_Description", "Ingrese la descripcion.") && isValid;

        if (isValid) {
            // Si todos los campos son válidos, puedes enviar el formulario aquí si lo deseas
            // $("#MaintenanceForm").submit();
        }
    });

    function validateField(selector, errorMessage) {
        var field = $(selector);
        // Verifica si el campo existe antes de intentar acceder a su valor
        if (field.length && field.val().trim() === '') {
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
