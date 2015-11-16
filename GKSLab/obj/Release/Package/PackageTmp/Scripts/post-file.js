//$(document).ready(function () {
//    $('#progressbar').hide();
//});
$(function () {
    document.getElementById('uploader').onsubmit = function () {
        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('fileInput');
        //Iterating through each files selected in fileInput
        for (i = 0; i < fileInput.files.length; i++) {
            //Appending each file to FormData object
            formdata.append(fileInput.files[i].name, fileInput.files[i]);
        }
        $('#progressbar').show();

        var xhr = new XMLHttpRequest();
        xhr.onprogress = progressHandlingFunction;
        xhr.onreadystatechange = function () {
            $('#partialContainer').html(xhr.responseText);

            if (xhr.readyState == 4 && xhr.status == 200) {
                alert(xhr.responseText);
                $('#progressbar').hide();
            }
        }
        xhr.open('POST', '/Lab/Count');
        xhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
        xhr.send(formdata);
    };
});

function progressHandlingFunction(e) {
    $('progress').attr({ value: e.loaded, max: e.total });
}

