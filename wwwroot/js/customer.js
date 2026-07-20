// Switch Between Links & Pages Functionality
const links = document.querySelectorAll("[data-page]");

const pages = {
    submit: document.getElementById("submit-page"),
    claims: document.getElementById("claims-page")
};

links.forEach(link => {
    link.addEventListener("click", async e => {
        e.preventDefault();

        Object.values(pages).forEach(page => page.hidden = true);

        pages[link.dataset.page].hidden = false;

        if (link.dataset.page === "claims"){
            await loadClaims();
        }
    });
});

// Submit Claim Functionality
const form = document.getElementById("claimForm");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const claim = {
        policyType: Number(document.getElementById("policyType").value),
        incidentDescription: document.getElementById("incidentDescription").value,
        incidentDate: document.getElementById("incidentDate").value,
        claimAmount: Number(document.getElementById("claimAmount").value)
    };

    console.log(claim);
    console.log(JSON.stringify(claim));

    const response = await fetch(`${api}/customer/claims`, {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(claim)
    });

    if (response.ok){
        alert("Claim submitted!");
    }

    // refresh cliams list after submiting new claims
    if (response.ok){
        form.reset();

        await loadClaims();
    }
})

// View Claims Functionality (for logged in user)
async function loadClaims() {

    const response = await fetch(`${api}/customer/claims`,
        {
            credentials: "include"
        }
    );

    if (!response.ok) return;

    const claims = await response.json();

    // populating claims table
    const table = document.getElementById("claimsTable");

    table.innerHTML = "";

    claims.forEach(claim => {

        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${claim.claimId}</td>
            <td>${claim.policyType}</td>
            <td>${claim.incidentDescription}</td>
            <td>${formatDate(claim.incidentDate)}</td>
            <td>${formatCurrency(claim.claimAmount)}</td>
            <td>${claim.status}</td>
            <td>${formatDate(claim.createdAt)}</td>
    `;

        table.appendChild(row);
    });   
}