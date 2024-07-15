

$(doucument).ready(function () {

    loadDataTable();
});

function loadDataTable() {

    dataTable = ('#myTable').DataTable({
        "ajax":
            { url: '/Inventory/GetAll' },
        "columns": [
            { data: 'items.product.name', "width":"25%" },
            { data: 'items.product.price' },
            { data: 'items.wareHouse.name' },
            { data: 'items.quantity' }
           
        ]
    });

}