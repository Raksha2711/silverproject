var PurchaseOrderUpdate = PurchaseOrderUpdate || {}
PurchaseOrderUpdate.selector = '.ajax[data-ns="PurchaseOrderUpdate"]';
PurchaseOrderUpdate.eventControlsSelector = '[data-ns="PurchaseOrderUpdate"]';
//PurchaseOrderUpdate.button = null;
//PurchaseOrderUpdate.data = null;
//PurchaseOrderUpdate.initialState = null;
//PurchaseOrderUpdate.daterange = null;
//PurchaseOrderUpdate.txtContractCode = null;
PurchaseOrderUpdate.init = function () {
    FlashSalesUpdate.ddlBoardBasisId = $('select[type=select][name=SalePersonName]');
}
DataContractUpdate.FillPaymentTerm = function () {
    $("#PaymentTerm").empty();
    var Days = 0;
    for (k = 1; k <= 50; k++) {
        Days += '<option value="' + k + '">' + k + '</option>';
    }
    $('#PaymentTerm').append(Days);
}

DataContractUpdate.FillSalesPerson = function () {
}
$(document).ready(function () {
    alert("1");
    PurchaseOrderUpdate.FillPaymentTerm();
    PurchaseOrderUpdate.FillSalesPerson();
    PurchaseOrderUpdate.FillVendor();
});