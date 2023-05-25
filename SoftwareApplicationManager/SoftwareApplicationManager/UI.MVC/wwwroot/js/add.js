window.addEventListener("load", initAddSoftwareApplication);
window.addEventListener('resize', addApplicationSetHeight);

function initAddSoftwareApplication()
{
    let addApplicationImage = document.querySelector('#addImageClick');
    addApplicationImage.addEventListener("click", addImage);

    let image = document.querySelector("#customImage");
    image.addEventListener("change", addPreviewImage);

    addApplicationSetHeight();
} // initAddSoftwareApplication.

function addApplicationSetHeight()
{
    // Set height of image container correctly.
    let element = $('#addApplicationImage');
    let elementY = element.offset().top;
    let width = element.width();
    element.css({'height': width + 'px'});
    
    let desc = $('#addDescription');
    let descY = desc.offset().top;
    desc.css({'height': (width - (descY - elementY)) + 'px'});

    $('#date').val('')

} // addApplicationSetHeight.

function addImage() 
{
    let imageInput = $('#customImage');
    imageInput.click();
} // addImage.

function addPreviewImage()
{
    
    let preview = document.querySelector('#imagePreview');
    let file    = document.querySelector('#customImage').files[0];
    let reader  = new FileReader();
    
    preview.style.display = "block";

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}
