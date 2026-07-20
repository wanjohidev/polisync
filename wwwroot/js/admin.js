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

    if (pageName === "policies") loadPolicies();
    if (pageName === "claims") loadClaims();
}

// Load Policies Functionality
async function loadPolicies(){

    const response = await fetch(`${api}/admins/policies`, {
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
            <td>${formatDate(policy.startDate)}</td>
            <td>${formatDate(policy.endDate)}</td>
            <td>${formatCurrency(policy.policyLimit)}</td>
        `;

        table.appendChild(row);
    });

}


// Load Claims Functionality
async function loadClaims(){

    const response = await fetch(`${api}/admins/claims`, {
        credentials: "include"
    });

    if (!response.ok) return;

    const claims = await response.json();

    const table = document.getElementById("claimsTable");

    table.innerHTML = "";

    claims.forEach(claim => {
        const row = document.createElement("tr");
        
        row.innerHTML = `
            <td>${claim.claimId}</td>
            <td>${claim.claimant}</td>
            <td>${claim.policyType}</td>
            <td>${claim.incidentDescription}</td>
            <td>${formatCurrency(claim.claimAmount)}</td>
            <td>${claim.status}</td>

            <td>
                <button 
                    class="view-btn"
                    data-id="${claim.claimId}"   
                >
                    View
                </button>
            </td>
        `;

        table.appendChild(row);
    });

    document.querySelectorAll(".view-btn")
            .forEach(btn => {
                btn.addEventListener("click", () => {
                    viewClaim(btn.dataset.id);
                });
            });
}

// View One Claim Functionality
async function viewClaim(claimId){
    const response = await fetch(`${api}/admins/claims/${claimId}`, {
        credentials: "include"
    });

    if (!response.ok) {
        alert("Unable to load claim.");
        return;
    }

    const claim = await response.json();

    selectedClaimId = claim.claimId;

    document.getElementById("detailClaimId").textContent = claim.claimId;
    document.getElementById("detailPolicy").textContent = claim.policyType;
    document.getElementById("detailClaimant").textContent = claim.claimant;
    document.getElementById("detailDescription").textContent = claim.incidentDescription;
    document.getElementById("detailAmount").textContent = formatCurrency(claim.claimAmount);
    document.getElementById("detailDate").textContent = formatDate(claim.incidentDate);
    document.getElementById("detailStatus").textContent = claim.status;
   
    document.getElementById("statusSelect").value = claim.status;

    document.getElementById("claimModal").showModal();
}

// Close Modal Functionality
document.getElementById("closeModal")
        .addEventListener("click", () => {
            document.getElementById("claimModal").close();
});

// Update Claim Functionality
document.getElementById("updateBtn")
        .addEventListener("click", updateClaim);

async function updateClaim() {
    if (selectedClaimId === null) return;

    const newStatus = document.getElementById("statusSelect").value;

    const response = await fetch(`${api}/admins/claims/${selectedClaimId}`, {
        method: "PUT",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ status:Number(newStatus) })
    });

    if (!response.ok) {
        alert("Unable to update claim status.");
        return;
    }
        
    // await viewClaim(selectedClaimId);
    // await loadClaims();
    document.getElementById("claimModal").close();
    await loadClaims();
}

// Delete Claim Functionality
document
    .getElementById("deleteBtn")
    .addEventListener("click", deleteClaim);

async function deleteClaim(){

    if (selectedClaimId === null) return;

    if (!confirm("Delete this claim?")) 
        return;

    const response = await fetch(`${api}/admins/claims/${selectedClaimId}`, {
        method: "DELETE",
        credentials: "include"
    });

    if (!response.ok) {
        alert("Unable to delete claim.");
        return;
    }

    document.getElementById("claimModal").close();

    await loadClaims();
}

// On Startup - when page loads
document.addEventListener("DOMContentLoaded", () => {
    loadPolicies(),
    loadClaims();
});
