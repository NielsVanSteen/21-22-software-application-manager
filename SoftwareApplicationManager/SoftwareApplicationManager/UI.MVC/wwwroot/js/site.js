window.addEventListener("load", init);

//Function executed on page load.
function init() 
{
    // Get the span element in the footer that has to display the year.
    let footerSpanYear = document.querySelector('.footer-span-year');
    footerSpanYear.innerHTML = new Date().getFullYear().toString();
    
    let modalClose = document.querySelectorAll(".modalClose");
    modalClose.forEach(s => s.addEventListener("click", closeModalBox));
    
}//init.

function closeModalBox()
{
    $(".modal").fadeOut(500);
    $(".modal-dialog").fadeOut(500).css({
        'top' : '0px'
    });
} // closeModalBox.

function openModalBox(title, text) 
{
    const modalTitle = document.querySelector("#modalTitle");
    modalTitle.innerHTML = title;

    const modalMessage = document.querySelector("#modalMessage");
    modalMessage.innerHTML = text;
    
    $(".modal").fadeIn(500);
    $(".modal-dialog").fadeIn(500).css({
        'top' : '100px'
    });
} // openModalBox.





