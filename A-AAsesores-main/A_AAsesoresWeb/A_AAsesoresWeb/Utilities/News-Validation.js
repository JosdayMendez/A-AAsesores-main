$(document).ready(function () {
    $('#FrequentQuestionsForm').parsley();

    $("#FrequentQuestionsForm").submit(function (event) {
        event.preventDefault();
        clearErrorMessages();
        var isValid = true;
        isValid = validateField("#NewsEnt_Title", "Por favor, ingrese título de la pregunta.") && isValid;
        isValid = validateField("#NewsEnt_Description", "Por favor, ingrese una respuesta a la pregunta.") && isValid;
        isValid = validateField("#NewsEnt_Url", "Por favor, ingrese el URL de la pregunta.") && isValid;
        // Puedes agregar más validaciones aquí según sea necesario...
        if (isValid) {
            // Realizar la llamada AJAX aquí
        }
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