$(document).ready(function () {
    Promise.all([GetCantons(), GetDistricts()]);

    $('#ProvinceId').change(function () {
        clearDropdowns();
        GetCantons(); // Se vuelve a cargar cada vez que se cambia la selección.
    });

    $('#CantonId').change(function () {
        GetDistricts(); // Se vuelve a cargar cada vez que se cambia la selección
    });
});

var isFirstTimeCantons = true;
var isFirstTimeDistricts = true;

document.getElementById('input-file-now-img').addEventListener('change', function (event) {
    const files = event.target.files;
    const allowedTypes = ['image/jpeg', 'image/png'];

    for (let i = 0; i < files.length; i++) {
        if (!allowedTypes.includes(files[i].type)) {
            Swal.fire({
                icon: 'error',
                title: 'Este tipo de archivo no es permitido.',
                text: 'Solo se permiten archivos JPG o PNG.',
            });
            event.target.value = ''; // Limpiar el campo de entrada
            break;
        }
    }
});

function clearDropdowns() {
    $('#CantonId').empty();
    $('#DistrictId').empty();
    isFirstTimeCantons = true;
    isFirstTimeDistricts = true;
}

function GetCantons() {
    let province = $("#ProvinceId").val();

    if (province.length > 0) {
        $.ajax({
            url: '/Properties/GetCantons?q=' + province,
            type: "GET",
            datatype: "json",
            success: function (data) {
                var cantonsDropdown = $('#CantonId');
                if (!isFirstTimeCantons) { // Verifica si es la primera vez
                    cantonsDropdown.empty(); // Limpiar dropdown solo si no es la primera vez
                } else {
                    isFirstTimeCantons = false; // Marcar como que ya no es la primera vez
                }
                $.each(data, function (index, item) {
                    if (!cantonsDropdown.find('option[value="' + item.Value + '"]').length) { // Verificar si la opción ya existe
                        cantonsDropdown.append($('<option></option>').attr('value', item.Value).text(item.Text));
                    }
                });
            },
            error: function (xhr, status, error) {
                console.log("Error al cargar los cantones");
                console.log(error)
            }
        });
    }
}

function GetDistricts() {
    let canton = $("#CantonId").val();

    if (canton.length > 0) {
        $.ajax({
            url: '/Properties/GetDistricts?q=' + canton,
            type: "GET",
            datatype: "json",
            success: function (data) {
                var districtsDropdown = $('#DistrictId');
                if (!isFirstTimeDistricts) { // Verifica si es la primera vez
                    districtsDropdown.empty(); // Limpiar dropdown solo si no es la primera vez
                } else {
                    isFirstTimeDistricts = false; // Marcar como que ya no es la primera vez
                }
                $.each(data, function (index, item) {
                    if (!districtsDropdown.find('option[value="' + item.Value + '"]').length) { // Verificar si la opción ya existe
                        districtsDropdown.append($('<option></option>').attr('value', item.Value).text(item.Text));
                    }
                });

                let distrito = $("#DistrictId").val();
            },
            error: function (xhr, status, error) {
                console.log("Error al cargar los distritos");
                console.log(error)
            }
        });
    }
}