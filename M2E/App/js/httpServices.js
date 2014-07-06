function httpGet(inputUrl, inputData) {
    return $.ajax({
        url: inputUrl,
        async: false
    }).responseText;
}

function httpPost(inputUrl, inputData) {
    return $.ajax({
        type: 'POST',
        data:inputData,
        url: inputUrl,
        async: false
    }).responseText;
}