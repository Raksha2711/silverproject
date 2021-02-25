var PurchaseOrderUpdate = PurchaseOrderUpdate || {}
PurchaseOrderUpdate.frm = $('form[name="frmAddBill"]')
PurchaseOrderUpdate.btn = $('button[type="button"].btn-submit')
PurchaseOrderUpdate.documentClick = function () {
    //$('select[name="Vendor"]').change(function () {
    //    alert("1");
    //    $("#ContactPerson").value = "12";
    //    $("#ContactNo").val("1k2");
    //    $("#EmailId").val("1j2");
    //});
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
        if (validate()) {
            $.get(baseurl + 'PurchaseOrder/additem', null, function (res) {
                $('.tblpo>tbody').append(res);
                setTimeout(addSerialNumber(), 100);
                $('.custome-select2').select2();
            });

        }

    });
    $(document).on('click', '.btn_row_delete', function (e) {
        var isCreate = PurchaseOrderUpdate.btn.attr('data-isupdate') == 'True'
        //alert(isCreate);
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
                    if (isCreate) {
                        $(this).parent().parent().remove();
                    }
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
                        var Id = $("#item_Id").val()
                        $.ajax({
                            type: 'POST',
                            url: baseurl + ('PurchaseOrder/deleterow/' + Id + '.json'),
                            data: JSON.stringify(frmData),
                            success: function (resut) { },
                            error: function (jqXHR) { debugger },
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8'
                        });
                    }

                } else {
                    swal("Your data is safe!");
                }
            });
        addSerialNumber();
    });
    // $(document).on('click', '.btn_row_delete', function (e) { $(this).parent().parent().remove() });
    $(document).on('click', '.btn-submit', function () {
        var frmData = getFromData();
        debugger
       // alert(frmData);
        var b1 = moment(frmData["Date"], 'YYYY-MM-DD').toDate()
        frmData["Date"] = b1;
        frmData.billItems = [];
        var isValid = true;
        var tr = $('.tblpo>tbody>tr');
        $.each(tr, function (k, v) {
            var trobj = {};
            var td = $(v).find(':input');
            $.each(td, function (tdk, tdv) {
                if (tdv.validity.valid) {
                    $(tdv).removeClass('is-invalid')
                    trobj[$(tdv).attr('name')] = $(tdv).val();
                }
                else {
                    $(tdv).addClass('is-invalid')
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) { return false; }
            frmData.billItems.push(trobj);
        })
        if (isValid)
            SaveOrUpdate(frmData);
    });

}

$(document).ready(function () {
    PurchaseOrderUpdate.documentClick();
    addSerialNumber();
    var today = new Date();
    //var minDate = new Date(today.getFullYear(), today, today.getMonth(), today.getDate() - 1);
    //var maxDate = new Date(today.getFullYear(), today, today.getMonth(), today.getDate() + 1);
    $('#Date').datepicker({
        format: "dd-mm-yyyy",
        orientation: "bottom auto",
        autoclose: true,
        todayHighlight: true,
        //setStartDate: minDate,
        //endDate: "12-02-2021",
    });
    $('.custome-select2').select2();
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
        success: function (resut) { window.location.href = baseurl + 'purchaseorder' },
        error: function (jqXHR) { debugger },
        dataType: 'json',
        contentType: 'application/json; charset=utf-8'
    });
}

function validate() {
    var $obj = $('.tblpo>tbody>tr:eq(0)');
    var form = $obj.find(':input');
    var result = true;
    form.each(function (k, v) {
        if (!v.validity.valid) {
            $(v).addClass('is-invalid')
            console.log('invalid:' + v.name);
            result = false;
            return false;
        }
        $(v).removeClass('is-invalid')
    })
    return result;
}
function addSerialNumber() {
    $('table tr').each(function (index) {
        $(this).find('td:nth-child(1)').html(index);
    });
};