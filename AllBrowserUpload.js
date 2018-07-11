
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>File upload page with Ajax</title>

    <script src="../Scripts/jquery-1.10.2.min.js"></script>

</head>
<body>


    <div>

        <div>
            <input type="file" name="UploadFile" id="txtUploadFile" class="makethispretty" />
        </div>


        <div id="son"></div>


        <div>

            <button id="btnUpload" onclick="uploadFile()">Upload File</button>
        </div>

        <div id="divResultContainer">
            <textarea id="content" rows="10" cols="100"></textarea>
        </div>
    </div>

    <script type="text/javascript">
        var files;
        $(document).ready(function () {

            $('#txtUploadFile').on('change', prepareToUpload);
            $('#btnUpload').on('click', uploadFile);
            function prepareToUpload(event) {
                files = event.target.files;
                console.log(files);
            }
            function uploadFile(event) {
                event.stopPropagation(); 
                event.preventDefault(); 
                var data = new FormData();
                $.each(files, function (key, value) {
                    data.append(key, value);
                });

                $.ajax({
                    url: '/FileUpload/Upload',
                    type: 'POST',
                    data: data,
                    cache: false,
                    dataType: 'json',
                    processData: false, 
                    contentType: false, 
                    success: function (serverResponse, textStatus, jqXHR) {
                        if (typeof data.error === 'undefined') {
                            $("#son").text(serverResponse);
                            console.log(serverResponse);
                        }
                        else {
                            console.log('ERRORS: ' + serverResponse.error);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log('ERRORS: ' + textStatus);
                       
                    }
                });
            }

        });
        
    </script>

</body>
</html>
