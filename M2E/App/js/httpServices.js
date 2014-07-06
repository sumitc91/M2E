function httpGet(inputUrl, inputData) {
    return $.ajax({
        url: inputUrl,
        async: false
    }).responseText;
}

function httpPost(inputUrl, inputData) {
    return $.ajax({
        type: 'POST',
        data: inputData,
        beforeSend: function () {
            $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> wait..</h1>' });
        },
        url: inputUrl,
        async: false,       
    }).success(function (data, status, headers, config) {        
        $.unblockUI();        
    }).error(function (data, status, headers, config) {
        
    }).responseText;
}