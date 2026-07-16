// Config
const api = "/api";
// keep track of whichever claim is currently selected
let selectedClaimId = null;

// Switch Between Links & Pages Functionality
const links = document.querySelectorAll("[data-page]");

const pages = {
    policies: document.getElementById("policies-page"),
    claims: document.getElementById("claims-page")
};

links.forEach(link => {
    link.addEventListener("click", async e => {
        e.preventDefault();
        showPage(link.dataset.page);
    });
});

function showPage(pageName){
    Object.values(pages).forEach(page => page.hidden = true);
    pages[pageName].hidden = false;

    document.getElementById("claimDetails").hidden = true;

    if (pageName === policies) loadPolicies();
    if (pageName === claims) loadClaims();
}

// Load Policies Functionality
async function loadPolicies(){
    const response = await fetch(`${api}/admins/policies`, {
        method: "GET",
        credentials: "include"
    });

    if (!response.ok) return;

    const policies = await response.json();
    const table = document.getElementById("policiesTable");

    table.innerHTML = "";

    policies.forEach(policy => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${policy.policyName}</td>
            <td>${policy.policyType}</td>
            <td>${policy.startDate}</td>
            <td>${policy.endDate}</td>
            <td>${policy.policyLimit}</td>
        `;

        table.appendChild(row);
    });

}


// Load Claims Functionality
async function loadClaims(){
    const response = await fetch(`${api}/admins/claims`, {
        method: "GET",
        credentials: "include"
    });

    if (!response.ok) return;

    const claims = await response.json();

    const table = document.getElementById("claimTable");
    table.innerHTML = "";

    claims.forEach(claim => {
        const row = document.createElement("tr");
        
        row.innerHTML = `
            <td>${claim.claimId}</td>
            <td>${claim.incidentDescription}</td>
            <td>${claim.claimAmount}</td>
            <td>${claim.claimant}</td>
            <td>${claim.policyType}</td>

            <td>
                <button 
                    class="view-btn"
                    data-id="${claim.id}"    
                >
                    View
                </button>
            </td>
        `;

        table.appendChild(row);
    });

    document
            .querySelectorAll(".view-btn")
            .forEach(btn => {
                btn.addEventListener("click", () => {
                    viewClaim(btn.dataset.id);
                });
            });
}

// View One Claim Functionality
async function viewClaim(claimId){
    const response = await fetch(`${api}/admins/claims/${claimId}`);

    if (!response.ok) return;

    const claim = await response.json();

    selectedClaimId = claimId;

    document.getElementById("claimDetails").hidden = false;

    document.getElementById("detailClaimId").textcontent = claim.id;
    document.getElementById("detailPolicy").textcontent = claim.policyType;
    document.getElementById("detailClaimant").textcontent = claim.user.name;
    document.getElementById("detailDescription").textcontent = claim.incidentDescription;
    document.getElementById("detailAmount").textcontent = claim.claimAmount;
    document.getElementById("detailStatus").value = claim.status;

    document.getElementById("statusSelect").value = claim.status;
}

// Update Claim Functionality
document
    .getElementById("updateBtn")
    .addEventListener("click", updateClaim);

async function updateClaim() {

    if (selectedClaimId === null) return;

    const status = document
            .getElementById("statusSelect")
            .value;

    const response = await fetch(`${api}/admins/claims/${selectedClaimId}`, {
        method: "PUT",
        headers: { "Content-Type": "application.json" },
        body: JSON.stringify({ status })
    });

    if (!response.ok) {
        alert("Unable to update claim.");
        return;
    }
        
    await viewClaim(selectedClaimId);
    await loadClaims();
}

// Delete Button Functionality
document
    .getElementById("deleteBtn")
    .addEventListener("click", deleteClaim);

async function deleteClaim(){

    if (selectedClaimId === null) return;

    if (!confirm("Delete this claim?")) return;

    const response = await fetch(`${api}/admins/claims/${selectedClaimId}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        alert("Unable to delete claim.");
        return;
    }

    document
        .getElementById("claimDetails")
        .hidden = true;

    selectedClaimId = null;

    await loadClaims();
}

// On Startup - when page loads
document.addEventListener("DOMContentLoaded", () => {
    loadClaims();
});
