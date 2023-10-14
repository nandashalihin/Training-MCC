

    $("#tablePoke").DataTable({
        ajax: {
            url: "https://localhost:7026/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },

        dom: 'Bifrtp',
        buttons: [


            {
                text: 'Export To Excel',
                extend: 'excelHtml5',
                className: 'btn btn-outline-success btn-sm',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                text: 'Export To PDF',
                extend: 'pdfHtml5',
                className: 'btn btn-outline-danger btn-sm',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                text: 'Choose Coloumn ',
                extend: 'colvis',
                className: 'btn btn-outline-secondary btn-sm',
                exportOptions: {
                    columns: ':visible'
                }
            },
        ],

        columns: [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "firstName" },
            { data: "lastName" },
            { data: "email" },
            { data: "phoneNumber" },
            { data: "birthDate" },
            {
                data: null,
                render: function (data, type, row) {
                    return '<button class="btn btn-info btn-sm" data-action="details">Details</button> ' +
                        '<button class="btn btn-danger btn-sm" data-action="delete" data-id="' + data.id + '">Delete</button> ' +
                        '<button class="btn btn-warning btn-sm" data-action="edit" >Edit</button>';
                }
            }
        ]

    });

function Insert() {
    var data = new Object();
    data.firstName = $("#firstName").val();
    data.lastName = $("#lastName").val();
    data.email = $("#email").val();
    data.phoneNumber = $("#phoneNumber").val();
    data.birthDate = $("#birthDate").val();
    data.gender = parseInt($("#gender").val());
    data.hiringDate = $("#hiringDate").val();

    /*var data = {
        firstName: firstName,
        lastName: lastName,
        email: email,
        phoneNumber: phone,
        birthDate: birthDate,
        gender: parseInt(gender),
        hiringDate: hiringDate
    };*/
    /*console.log(data);*/
        $.ajax({
            url: 'https://localhost:7026/api/Employee',
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
        }).done(function (result) {
            alert('Data berhasil disimpan');
            window.history.back();
        }).fail(function (error) {
            alert('Data gagal disimpan');
            window.history.back();
        });
    }

$(document).on('click', 'button[data-action="delete"]', function () {
    if (confirm('Apakah Anda yakin ingin menghapus karyawan ini?')) {
        var data = $('#tablePoke').DataTable().row($(this).parents('tr')).data();
        var guidEmployee = data.guid; // Gantilah dengan bidang ID karyawan yang sesungguhnya
        // Buat permintaan AJAX untuk menghapus karyawan
        $.ajax({
            url: 'https://localhost:7026/api/Employee/' + guidEmployee,
            type: 'DELETE',
        }).done(function (result) {
            alert('Data berhasil dihapus');
            // Perbarui DataTable setelah menghapus catatan
            $('#tablePoke').DataTable().ajax.reload();
        }).fail(function (error) {
            alert('Data gagal dihapus');
        });
    }
});

function fillFormWithEmployeeData(employeeData) {
    // Mengisi setiap elemen formulir dengan data yang sesuai
    $("#editFirstName").val(employeeData.firstName);
    $("#editLastName").val(employeeData.lastName);

    // Mengisi tanggal lahir dengan format yyyy-mm-dd
    if (employeeData.birthDate) {
        var birthDate = new Date(employeeData.birthDate);
        if (!isNaN(birthDate.getTime())) {
            var formattedBirthDate = birthDate.toISOString().substr(0, 10);
            $("#editBirthDate").val(formattedBirthDate);
        }
    }

    // Mengisi tanggal perekrutan dengan format yyyy-mm-dd
    if (employeeData.hiringDate) {
        var hiringDate = new Date(employeeData.hiringDate);
        if (!isNaN(hiringDate.getTime())) {
            var formattedHiringDate = hiringDate.toISOString().substr(0, 10);
            $("#editHiringDate").val(formattedHiringDate);
        }
    }

    $("#editEmail").val(employeeData.email);
    $("#editPhoneNumber").val(employeeData.phoneNumber);
    $("#editGender").val(employeeData.gender);

    // Mengisi input tersembunyi dengan GUID karyawan yang sedang diedit
    $("#editEmployeeId").val(employeeData.guid);
    $("#editEmployeeNik").val(employeeData.nik);
    console.log(employeeData);

    // Menampilkan modal edit
    var myModal = new bootstrap.Modal(document.getElementById('editEmployeeModal'));
    myModal.show();
}


$(document).on('click', 'button[data-action="edit"]', function () {
    var data = $('#tablePoke').DataTable().row($(this).parents('tr')).data();
    /*console.log("Edit button clicked");
    console.log(data);*/
    fillFormWithEmployeeData(data);
});



function saveEmployee() {
    var data = new Object();
    data.firstName = $("#editFirstName").val();
    data.lastName = $("#editLastName").val();
    data.email = $("#editEmail").val();
    data.phoneNumber = $("#editPhoneNumber").val();
    data.birthDate = $("#editBirthDate").val();
    data.gender = parseInt($("#editGender").val());
    data.hiringDate = $("#editHiringDate").val();
    data.guid = $("#editEmployeeId").val();
    data.nik = $("#editEmployeeNik").val();
    console.log(data);
    $.ajax({
        url: 'https://localhost:7026/api/Employee',
        type: "PUT",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
    }).done(function (result) {
        alert('Data berhasil disimpan');
        location.reload(); 
    }).fail(function (error) {
        alert('Data gagal disimpan');
        window.history.back();
    });
}




