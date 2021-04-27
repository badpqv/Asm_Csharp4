var previewImage = function(event) {
    var imgsrc = document.getElementById("inputSrc").value;
    var imgPreviewer = document.getElementById("imgPreviewer");
    imgPreviewer.src = imgsrc;
}