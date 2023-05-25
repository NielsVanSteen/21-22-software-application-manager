
// Execute on page load.
window.addEventListener("load", function() {
    let id = getIdFromUrl();
    loadDeveloperDetail(id);
    loadAllRatings();
});

// Get the id from the url.
function getIdFromUrl()
{
    let url = window.location.pathname;
    return url.split('/').reverse()[0];
} // getIdFromUrl.

// Load the developer info with ajax and json.
function loadDeveloperDetail(id)
{
    
    fetch("https://localhost:5001/api/Developers/"+id, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showDeveloper(data))
        .catch(reason => alert("Call failed: " + reason));
} // loadDeveloperDetail.

function loadAllRatings()
{
    fetch("https://localhost:5001/api/Ratings/"+getIdFromUrl(), {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showRatings(data))
        .catch(reason => alert("Call failed: " + reason));
} // loadAllRatings.

function showRatings(ratings)
{
    let html = "<h2>Ratings of Developer</h2>";
    
    for (let i=0; i<ratings.length; i++)
    {
        const rating = ratings[i];
        
        html += `
            <section class="item-overview item-overview-no-image item-overview-no-description-height">
                <div class="item-other-wrapper">
                    <h3>${rating.title}</h3>
                    <p class="item-overview-description">
                       ${rating.description}</
                    </p>
                    <ul class="item-tags">
                        <li>${rating.postedOn.split("T")[0]}</</li>
                        <li>${rating.postedOn.split("T")[1].split(".")[0]}u</li>
                    </ul>
                    <div class="item-inner-container">
                       
                    </div>
                </div>
            </section>
        `;
    } // For.
    document.querySelector("#developerAllRatings").innerHTML = html;
    
} // showRatings.

// Ouput all the data on the screen.
function showDeveloper(developer)
{
    showDeveloperMain(developer);
    showDeveloperContactInfo(developer);
    
    // Button is added to html content with javascript in function 'showDeveloperMain' -> this must be invoked after that function.
    addEventListenerSaveDeveloper(developer.developerId);

    loadSoftwareApplications();
} // showDeveloper.

// Ouput the main developer info.
function showDeveloperMain(developer)
{
    const developerDetailWrapper = document.querySelector("#developerDetailWrapper");

    let html = `
        <figure>
            <img src="../../../media/images/${developer.profilePicture}" alt="${developer.name}" class="main-detail-image" id="developerImage">
        </figure>
        <div class="main-detail-info-container">
            <h3> <input type="text" value="${developer.name}" name="developerName" id="developerName"> </h3>
            <p class="date"> <i class="far fa-calendar-minus"></i> <input type="date" value="${developer.birthDate.split("T")[0]}" name="developerBirthDate" id="developerBirthDate"> </p>
            <p class="description"><textarea name="developerDescription" id="developerDescription">${developer.description}</textarea></p>
            <button class="btn btn-success" id="developerSave" type="button">Save Changes</button>
        </div>
    `;

    developerDetailWrapper.innerHTML = html;
} // showDeveloperMain.

// Show the contact info of the developer.
function showDeveloperContactInfo(developer)
{
    const developerDetailContactWrapper = document.querySelector("#developerDetailContactWrapper");
    
    let html = `
        <section class="contact-container">
            <p class="contact-title"> <i class="fas fa-phone"></i> Phone</p>
            <p><input type="text" value="${developer.phoneNumber}" name="developerPhoneNumber" id="developerPhoneNumber"></p>
        </section>
        <!-- Address section is omitted. Sadly it doesn't look as good now. -->
        <section class="contact-container">
            <p class="contact-title"> <i class="far fa-envelope"></i> E-mail</p>
            <p><input type="text" value="${developer.email}" name="developerEmail" id="developerEmail"></p>
        </section>
        `;
    
    developerDetailContactWrapper.innerHTML = html;
} // showDeveloperContactInfo.

// Add eventlistner for saving a developer.
function addEventListenerSaveDeveloper(id)
{
  const btnSaveDeveloper = document.querySelector("#developerSave");
  btnSaveDeveloper.addEventListener("click", updateDeveloper, false);
  btnSaveDeveloper.developerId = id;
  
  const btnWriteRating = document.querySelector("#btnWriteRating");
  btnWriteRating.addEventListener("click", createRating);
  
} // addEventListenerSaveDeveloper.

// Save Update The developer.
function updateDeveloper(evt)
{
    const id = evt.currentTarget.developerId;
    
    const developer = {
        developerId: id,
        name: document.querySelector("#developerName").value,
        description: document.querySelector("#developerDescription").value,
        profilePicture: document.querySelector("#developerImage").src.split("/").reverse()[0],
        birthDate: document.querySelector("#developerBirthDate").value,
        phoneNumber: document.querySelector("#developerPhoneNumber").value,
        email: document.querySelector("#developerEmail").value
    }
    
    
    fetch("https://localhost:5001/api/Developers/"+id, {
        method: "PUT",
        body: JSON.stringify(developer),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => updatedDeveloper(response))
        .catch(() => alert('Call failed'));
    
} // updateDeveloper.

function updatedDeveloper(response)
{
    let title = "Update Developer";
    let text;
    
    if (response.ok)
        text = "Developer has successfully been updated!"
    else
        text = "Failed to update Developer: ";
    openModalBox(title, text);
} // updatedDeveloper.


function loadSoftwareApplications()
{
    fetch("https://localhost:5001/api/SoftwareApplications", {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showSoftwareApplications(data))
        .catch(reason => alert("Call failed: " + reason));
} // loadSoftwareApplications.

function showSoftwareApplications(softwareApplications)
{
    let html = "";

    softwareApplications.forEach(s => html += ` <option value="${s.softwareApplicationId}">${s.name}</option>`);

    document.querySelector("#ratingApplication").innerHTML = html;
} // showDevelopers.

function createRating()
{
    const rating = {
        developerId: getIdFromUrl(),
        softwareApplicationId: document.querySelector("#ratingApplication").value,
        title: document.querySelector("#ratingTitle").value,
        description: document.querySelector("#ratingDescription").value,
        score: document.querySelector("#ratingScore").value,
        postedOn: new Date()
    }

    fetch("https://localhost:5001/api/Ratings", {
        method: "POST",
        body: JSON.stringify(rating),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            createdRating(response);
            console.log(response);
        }).catch(r => {
            alert(r);
            createdRating(Response.error());
            console.log(r);
        });
    
} // createRating.

function createdRating(response)
{
    let title = "Write a Rating";
    let text;
    
    console.log(response);

    if (response.ok)
        text = "Your rating has successfully been posted!"
    else
        text = "Failed to publish rating!";

    loadAllRatings();
    openModalBox(title, text);
}


















