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

const downloadToFile = (content, filename, contentType) => {
    const a = document.createElement('a');
    const file = new Blob([content], { type: contentType });

    a.href = URL.createObjectURL(file);
    a.download = filename;
    a.click();

    URL.revokeObjectURL(a.href);
};