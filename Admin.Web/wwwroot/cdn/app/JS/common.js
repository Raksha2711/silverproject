var $$ = $$ || {};
$$.dataTable = function (selector, options) {
    var $table = $(selector);
    var data = $table.data();
    var list_url = data.url; //+ '/list.json';
    var filterData = $(selector).data();


    var defaults = {
        "processing": true,
        "serverSide": true,
        "info": true,
        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, 'All']],
        "ajax": {
            "url": list_url,
            "type": "POST",
            "data": function (param) {
                //param.myKey = "myValue";
                $.each(filterData, function (fdk, fdv) {
                    if (fdk.toLowerCase().indexOf('filter') > -1 && ((typeof fdv == 'string' && fdv.length > 0) || fdv > 0)) {
                        var fdkk = fdk.replace('filter', '');
                        for (var c = 0; c < param.columns.length; c++) {
                            if (param.columns[c].name.toLowerCase() == fdkk.toLowerCase()) {
                                param.columns[c].search.value = fdv;
                                break;
                            }
                        }
                    }
                });
                var $frmFilter = $('form.filter');
                var frmFilterData = {};
                if ($frmFilter.length > 0) {
                    frmFilterData = $$.getFormData($frmFilter);
                    console.log('$frmFilter > frmFilterData', frmFilterData)
                }

                $.each(frmFilterData, function (fdk, fdv) {
                    for (var c = 0; c < param.columns.length; c++) {
                        if (param.columns[c].name.toLowerCase() == fdk.toLowerCase()) {
                            param.columns[c].search.value = fdv;
                            break;
                        }
                    }
                });

                console.log('filter', param);
            }
        },
        /*"columns": options.columns,*/
        "order": [[1, "asc"]],
        "initComplete": function (settings, json) {

            //setTimeout(function () {
            //    //$('.DTFC_RightBodyWrapper').css('background-color', '#F2F2F2');
            //    $('.table.dataTable.DTFC_Cloned').css('background-color', '#F2F2F2');
            //    $('.DTFC_RightBodyWrapper').css('top', '-13px');
            //    //$('.table.dataTable.DTFC_Cloned').css('margin', '0px!important');
            //    //$('.table.DTFC_Cloned').css('margin-bottom', '0px!important');
            //}, 100);
            //setTimeout(function () {
            //    $('table.dataTable:not(.DTFC_Cloned) tbody td:last-child').html('');
            //}, 1000);

        },
        "drawCallback": function (settings) {
            //console.log('drawCallback._oFixedColumns', settings._oFixedColumns);
            if (settings._oFixedColumns != undefined) {
                setTimeout(function () {
                    //$('.DTFC_RightBodyWrapper').css('background-color', '#F2F2F2');
                    $('.table.dataTable.DTFC_Cloned').css('background-color', '#F2F2F2');
                    $('.DTFC_RightBodyWrapper').css('top', '-13px');
                    //$('.table.dataTable.DTFC_Cloned').css('margin', '0px!important');
                    //$('.table.DTFC_Cloned').css('margin-bottom', '0px!important');
                }, 100);
                setTimeout(function () {
                    $('table.dataTable:not(.DTFC_Cloned) tbody td:last-child').html('');
                }, 1000);
            }
        },
        "fnRowCallback": function (nRow, aData, iDisplayIndex) {
            var oSettings = table.fnSettings();
            // console.log('oSettings', oSettings);
            //$("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
            return nRow;
        }
    };
    var config = $.extend({}, defaults, options);
    var table = $(selector).dataTable(config);

    //$('.filter-cancel').click(function () {
    //    table.api().columns().every(function () {
    //        this.search('');
    //    });
    //    $('.datatableTextInput').val('');
    //    table.fnDraw();
    //});
    //$('.filter-submit').click(function () {
    //    table.fnDraw();
    //});

    //$('.datatableTextInput').on('keyup change', function () {
    //    var columnName = $(this).attr('name');
    //    var column = table.api().column(columnName + ':name');
    //    if (column.search() !== this.value) {
    //        column.search(this.value.trim().toLowerCase());
    //    }
    //});
    $.each($('select[data-ddlurl]'), function (i, v) {
        $$.dropdown($(this));
    });
    $('form.filter').on('click', '#btnApplyFilter', function (e) {
        table.fnDraw();
    });
    

    var $iboxtoolbar = $(selector).parents('.ibox').find('.ibox-toolbar');
    var iboxtoolbarData = $iboxtoolbar.data();
    if (!!iboxtoolbarData && iboxtoolbarData.buttons != undefined) {
        var iButtons = iboxtoolbarData.buttons.split(',');
        
        var queryString = window.location.search;
        var tdata = { href: data.url + '/0' + queryString };
        $.each(iButtons, function (i, v) {
            if (listButtonTemplates[v] != undefined) {
                var tbar = $$.render(listButtonTemplates[v], tdata)
                console.log(tbar);
                $iboxtoolbar.append(tbar);
            }
        });

        //var tbart = '<a class="btn btn-primary btn-flat" href="{href}"><i class="fa fa-lg fa-plus"></i> Add</a>';
        //var tbar = $$.render(tbart, tdata)
        //console.log(tbar);
        //$iboxHead.append(tbar);
    }
    return table;
};

$$.getFormData = function ($form) {
    
    var o = {};
    var a = $form.serializeArray();
    //if (typeof (CKEDITOR) !== "undefined") {
    //    for (var i in CKEDITOR.instances) {
    //        if (i.length > 0) {
    //            var _value = CKEDITOR.instances[i].element.hasClass('get-html') ? CKEDITOR.instances[i].getData() : CKEDITOR.instances[i].document.getBody().getText();
    //            _value.length > 1 ? a.find(input => input.name == CKEDITOR.instances[i].name).value = _value : "";
    //        }
    //    };
    //}
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
            o[this.name] = o[this.name].join(',');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};