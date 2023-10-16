$(document).ready(function () {
    // Inisialisasi DataTable dengan pengaturan dasar
    $("#tablePoke").DataTable({
        ajax: {
            url: "https://localhost:7026/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        dom: 'Bifrtp', // Mengatur elemen-elemen tampilan DataTable
        buttons: [ // Mengkonfigurasi tombol aksi
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
                text: 'Choose Column ',
                extend: 'colvis',
                className: 'btn btn-outline-secondary btn-sm',
                exportOptions: {
                    columns: ':visible'
                }
            },
        ],
        columns: [ // Konfigurasi kolom dalam tabel
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: "firstName",
                width: "25%",
                // Lebar sesuai dengan col-3 (25% dari total lebar tabel)
                render: function (data) {
                    // Menggunakan overflow dan text ellipsis untuk teks yang panjang
                    return '<div class=" text-break">' + data + '</div>';
                }
            },
            { data: "lastName" },
            { data: "nik" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "birthDate",
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        var date = new Date(data);
                        var day = date.getDate().toString().padStart(2, '0'); // Tambah '0' jika hanya satu digit
                        var month = (date.getMonth() + 1).toString().padStart(2, '0'); // Tambah '0' jika hanya satu digit
                        var year = date.getFullYear();
                        return day + '/' + month + '/' + year;
                    }
                    return data; // Untuk jenis lainnya, kembalikan data asli
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<div class="text-center text-middle text-break">' +
                        '<button class="btn btn-info btn-sm" data-action="detail">Details</button> ' +
                        '<button class="btn btn-danger btn-sm" data-action="delete" data-id="' + data.id + '">Delete</button> ' +
                        '<button class="btn btn-warning btn-sm" data-action="edit">Edit</button>' +
                        '</div>';
                }
            }
        ],
        createdRow: function (row, data, dataIndex) {
            // Dapatkan semua elemen <td> dalam baris ini
            var tdElements = $(row).find('td');

            // Loop melalui setiap elemen <td> dan tambahkan kelas "text-break"
            tdElements.addClass('text-break');
        }
    });




    // Fungsi untuk menghapus data karyawan
    $(document).on('click', 'button[data-action="delete"]', function () {
        Swal.fire({
            title: 'Konfirmasi',
            text: 'Apakah Anda yakin ingin menghapus karyawan ini?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Ya, Hapus',
            cancelButtonText: 'Batal',
        }).then((result) => {
            if (result.isConfirmed) {
                var data = $('#tablePoke').DataTable().row($(this).parents('tr')).data();
                var guidEmployee = data.guid; // Mengambil ID karyawan yang akan dihapus
                // Mengirim permintaan AJAX untuk menghapus karyawan
                $.ajax({
                    url: 'https://localhost:7026/api/Employee/' + guidEmployee,
                    type: 'DELETE',
                }).done(function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Data berhasil dihapus',
                        showConfirmButton: false,
                        timer: 1500 // Menutup pesan setelah 1.5 detik
                    });
                    // Memuat ulang DataTable setelah menghapus catatan
                    $('#tablePoke').DataTable().ajax.reload();
                }).fail(function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Data gagal dihapus',
                        text: 'Terjadi kesalahan saat menghapus data.',
                    });
                });
            }
        });
    });

    // Fungsi untuk mengisi formulir edit dengan data karyawan yang akan diubah
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

        // Menampilkan modal edit
        var myModal = new bootstrap.Modal(document.getElementById('editEmployeeModal'));
        myModal.show();
    }

    // Menangani klik tombol "Edit" untuk mengedit data karyawan
    $(document).on('click', 'button[data-action="edit"]', function () {
        var data = $('#tablePoke').DataTable().row($(this).parents('tr')).data();
        fillFormWithEmployeeData(data);
    });




    // Fungsi untuk mengisi modal detail dengan data karyawan
    function fillModalWithEmployeeData(employeeData) {
        // Mengisi setiap elemen formulir dengan data yang sesuai
        $("#detailFirstName").text(": " + employeeData.firstName);
        $("#detailLastName").text(": " + employeeData.lastName);

        // Mengisi tanggal lahir dengan format yyyy-mm-dd
        if (employeeData.birthDate) {
            var birthDate = new Date(employeeData.birthDate);
            if (!isNaN(birthDate.getTime())) {
                var formattedBirthDate = birthDate.toISOString().substr(0, 10);
                $("#detailBirthDate").text(": " + formattedBirthDate);
            }
        }

        // Mengisi tanggal perekrutan dengan format yyyy-mm-dd
        if (employeeData.hiringDate) {
            var hiringDate = new Date(employeeData.hiringDate);
            if (!isNaN(hiringDate.getTime())) {
                var formattedHiringDate = hiringDate.toISOString().substr(0, 10);
                $("#detailHiringDate").text(": " + formattedHiringDate);
            }
        }

        $("#detailEmail").text(": " + employeeData.email);
        $("#detailPhoneNumber").text(": " + employeeData.phoneNumber);
        var genderText = (employeeData.gender === 1) ? "Laki-Laki" : (employeeData.gender === 0) ? "Perempuan" : "Tidak Diketahui";
        $("#detailGender").text(": " + genderText);

        // Mengisi input tersembunyi dengan GUID karyawan yang sedang dilihat detailnya
        $("#detailEmployeeId").text(": " + employeeData.guid);
        $("#detailEmployeeNik").text(": " + employeeData.nik);

        // Menampilkan modal detail
        var myModal = new bootstrap.Modal(document.getElementById('detailEmployeeModal'));
        myModal.show();
    }

    // Menangani klik tombol "Details" untuk melihat detail data karyawan
    $(document).on('click', 'button[data-action="detail"]', function () {
        var data = $('#tablePoke').DataTable().row($(this).parents('tr')).data();
        fillModalWithEmployeeData(data);
    });


});


// Fungsi untuk menambahkan data karyawan
function Insert() {
    var data = new Object();
    data.firstName = $("#firstName").val();
    data.lastName = $("#lastName").val();
    data.email = $("#email").val();
    data.phoneNumber = $("#phoneNumber").val();
    data.birthDate = $("#birthDate").val();
    data.gender = parseInt($("#gender").val());
    data.hiringDate = $("#hiringDate").val();

    // Mengirim data karyawan baru ke server melalui AJAX POST
    $.ajax({
        url: 'https://localhost:7026/api/Employee',
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
    }).done(function (result) {
        Swal.fire({
            icon: 'success',
            title: 'Data berhasil disimpan',
            showConfirmButton: false,
            timer: 1500 // Menutup pesan setelah 1.5 detik
        });

        $('#employeeModal').modal('hide');

        // Memuat ulang DataTable
        $('#tablePoke').DataTable().ajax.reload();
    }).fail(function (error) {
        Swal.fire({
            icon: 'error',
            title: 'Data gagal disimpan',
            text: 'Terjadi kesalahan saat menyimpan data.',
        });
    });
}

// Fungsi untuk menyimpan perubahan data karyawan setelah diedit
function saveEmployee() {
    let data = new Object();
    data.firstName = $("#editFirstName").val();
    data.lastName = $("#editLastName").val();
    data.email = $("#editEmail").val();
    data.phoneNumber = $("#editPhoneNumber").val();
    data.birthDate = $("#editBirthDate").val();
    data.gender = parseInt($("#editGender").val());
    data.hiringDate = $("#editHiringDate").val();
    data.guid = $("#editEmployeeId").val();
    data.nik = $("#editEmployeeNik").val();

    // Mengirim data yang diperbarui ke server melalui AJAX PUT
    $.ajax({
        url: 'https://localhost:7026/api/Employee',
        type: "PUT",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
    }).done(function (result) {
        Swal.fire({
            icon: 'success',
            title: 'Data berhasil disimpan',
            showConfirmButton: false,
            timer: 1500 // Menutup pesan setelah 1.5 detik
        });

        $('#editEmployeeModal').modal('hide');
        // Memuat ulang DataTable
        $('#tablePoke').DataTable().ajax.reload();
    }).fail(function (error) {
        Swal.fire({
            icon: 'error',
            title: 'Data gagal disimpan',
            text: 'Terjadi kesalahan saat menyimpan data.',
        });
    });
}
