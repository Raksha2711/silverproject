var PurchaseOrderUpdate = PurchaseOrderUpdate || {}
PurchaseOrderUpdate.frm = $('form[name="frmAddBill"]')
PurchaseOrderUpdate.btn = $('button[type="button"].btn-submit')
PurchaseOrderUpdate.documentClick = function () {

    $('select[name="PaymentTerm"]').change(function () {
        if ($(this).val() == "PDC") {
            // $('input[name="PaymentValue"]').show();
            $('#divpayment').show();
            $('input[name="PaymentValue"]').val(30);
        }
        else {
            $('#divpayment').hide();
          //  $('input[name="PaymentValue"]').hide();
            $('input[name="PaymentValue"]').val(0);
        }
    });
    $('select[name="DeliveryType"]').change(function () {
        if ($(this).val() == "Delivery") {
            $("#divaddress").show();
        }
        else {
            $("#divaddress").hide();
        }
    });
    $('.btn-add-item').on('click', function () {
        $.get(baseurl + 'PurchaseOrder/additem', null, function (res) {
            $('.tblpo>tbody').append(res);
        });
    });
    $(document).on('click', '.btn_row_delete', function (e) {
        var isCreate = PurchaseOrderUpdate.btn.attr('data-isupdate') == 'True'
        alert(isCreate);
        //$(this).parent().parent().remove()
    
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this data!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    swal("Poof! Your imaginary file has been deleted!", {
                        icon: "success",
                    });
                    debugger
                    if (isCreate) {
                        $(this).parent().parent().remove();}
                    else {
                        alert("else");
                        var frmData = getFromData();
                        frmData.billItems = [];
                        var tr = $('.tblpo>tbody>tr');
                        $.each(tr, function (k, v) {
                            var trobj = {};
                            var td = $(v).find(':input');
                            $.each(td, function (tdk, tdv) { trobj[$(tdv).attr('name')] = $(tdv).val(); });
                            frmData.billItems.push(trobj);
                        })
                        alert(frmData.Id);
                        $.ajax({
                            type: 'POST',
                            url: baseurl + ('PurchaseOrder/deleterow/' + frmData.Id + '.json'),
                            data: JSON.stringify(frmData),
                            success: function (resut) {  },
                            error: function (jqXHR) { debugger },
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8'
                        });
                    }
                   
                } else {
                    swal("Your data is safe!");
                }
            });
    
});
   // $(document).on('click', '.btn_row_delete', function (e) { $(this).parent().parent().remove() });
    $(document).on('click', '.btn-submit', function () {
        var frmData = getFromData();
        frmData.billItems = [];
        var tr = $('.tblpo>tbody>tr');
        $.each(tr, function (k, v) {
            var trobj = {};
            var td = $(v).find(':input');
            $.each(td, function (tdk, tdv) { trobj[$(tdv).attr('name')] = $(tdv).val(); });
            frmData.billItems.push(trobj);
        })
        SaveOrUpdate(frmData);
        debugger;
    })
}

$(document).ready(function () {
    PurchaseOrderUpdate.documentClick();
});

function getFromData() {
    var data = {};
    $.each(PurchaseOrderUpdate.frm.serializeArray(), function (k, v) {
        data[v.name] = v.value;
    });
    return data;
}
function SaveOrUpdate(obj) {
    var isCreate = PurchaseOrderUpdate.btn.attr('data-isupdate') == 'True'
    $.ajax({
        type: 'POST',
        url: baseurl + (!!isCreate ? 'PurchaseOrder/create.json' : 'PurchaseOrder/update/' + obj.Id + '.json'),
        data: JSON.stringify(obj),
        success: function (resut) {window.location.href = baseurl + 'purchaseorder' },
        error: function (jqXHR) { debugger },
        dataType: 'json',
        contentType: 'application/json; charset=utf-8'
    });
}