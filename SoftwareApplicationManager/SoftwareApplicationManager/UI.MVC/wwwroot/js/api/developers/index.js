// Global variables.
const loadDevelopersButton = document.querySelector("#loadDevelopers");

// Execute on page load.
window.addEventListener("load", function() {
    loadDevelopersButton.addEventListener("click", loadDevelopers);
    loadDevelopers();
});

// Loading Developers, and displaying them in the browser.
function loadDevelopers()
{
    fetch("https://localhost:5001/api/Developers", {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showDevelopers(data))
        .catch(reason => alert("Call failed: " + reason)); 
} // loadDevelopers.

function showDevelopers(developers)
{
    const developersTable = document.querySelector("#developersContainer");
    let html = "";
    
    for (let i=0; i<developers.length; i++) 
    {
        let developer = developers[i];
        
        html += `
            <section class="item-overview">
                <figure class="item-figure">
                    <img src="../../../media/images/${developer.profilePicture}" alt="" class="item-image">
                </figure>
                <div class="item-other-wrapper">
                    <h3>${developer.name}</h3>
                    <p class="item-overview-description">
                        ${developer.description}
                    </p>
                    <ul class="item-tags">
                        <li>${developer.email}</li>
                        <li>${developer.phoneNumber}</li>
                    </ul>
                    <div class="item-inner-container">
                        <div class="item-score">
                            ${developer.birthDate.split("T")[0]}
                        </div>
                        <div class="item-details-container">
                            <a href="https://localhost:5001/Developer/Detail/${developer.developerId}" class="item-details-link">Details</a>
                        </div>
                    </div>
                </div>
            </section>
        `;
    } // For.

    developersTable.innerHTML = html;
} // showDevelopers.
