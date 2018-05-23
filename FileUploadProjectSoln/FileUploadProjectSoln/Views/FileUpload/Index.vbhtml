@Code
    ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<div>
    <a href="~/html/FileUploader.html">Upload File</a>
</div>

<form name="form1" method="post" enctype="multipart/form-data" action="Push">
    <div>
        <label for="caption">Image Caption</label>
        <input name="caption" type="text" />
    </div>
    <div>
        <label for="image1">Image File</label>
        <input name="image1" type="file" />
    </div>
    <div>
        <input type="submit" value="Submit" />
    </div>
</form>

<div id="containerDiv">Processsing Please Wait ...</div>