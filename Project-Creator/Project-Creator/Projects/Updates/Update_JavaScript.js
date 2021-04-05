function txtDesc_TextChanged() {
    val = document.getElementById("txtDesc").value;
    document.getElementById("lblDescCounter").innerHTML = val.length + " of 255";
}

function previewTimelineImage() {
    const preview = document.getElementById("timeline_image");
    const file = document.getElementById('ImageUploader').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

function previewFile() {
    var preview = null;
    const file = document.getElementById('ContentUploader').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        document.getElementById("<%=FileImage.ClientID %>").style.display = "none";
        document.getElementById("<%=FileVideo.ClientID %>").style.display = "none";
        document.getElementById("<%=FilePDF.ClientID %>").style.display = "none";
        document.getElementById("<%=FileText.ClientID %>").style.display = "none";
        var pdf = false;
        switch (file.type) {
            case ("image/jpeg"):
            case ("image/png"):
            case ("image/bmp"):
                document.getElementById("<%=FileImage.ClientID %>").style.display = "block";
                preview = document.getElementById("image_upload");
                break;
            case ("application/pdf"):
                document.getElementById("<%=FilePDF.ClientID %>").style.display = "block";
                preview = document.getElementById("viewer");
                reader.readAsDataURL(file);
                document.getElementById("wrapper").data = reader.result;
                pdf = true;
                break;
            case ("video/mp4"):
                document.getElementById("<%=FileVideo.ClientID %>").style.display = "block";
                preview = document.getElementById("video_upload");
                break;
            case ("text/plain"):
                document.getElementById("<%=FileText.ClientID %>").style.display = "block";
                preview = document.getElementById("PDF_upload");
                break;
        }

        if (pdf == false) {
            reader.readAsDataURL(file);
        }

    }
}