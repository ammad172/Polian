var table;
$(document).ready(function (e) {
    LoadTable();
});

function LoadTable() {
    table = $('#myTable').DataTable({
        ajax: {
            url: '/Order/GetAll',
            dataSrc: 'data',
            contentType: 'application/json',
            dataType: "JSON",
        },
        columns: [
            { data: 'username', "width": '5%' },
            { data: 'couponCode', "width": '5%' },
            { data: 'discount', "width": '5%' },
            { data: 'orderTotal', "width": '5%' },
            { data: 'name', "width": '5%' },
            { data: 'phoneNumber', "width": '5%' },
            { data: 'email', "width": '5%' },
            { data: 'orderDateTime', "width": '5%' },
            { data: 'status', "width": '5%' },
        ]
    });
}