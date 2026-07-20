const api = "/api";

// Logout Functionality
document.getElementById("logoutBtn")
        .addEventListener("click", logout);
    
async function logout(){
    const result = await fetch(`${api}/auth/logout`, {
        method: "POST",
        credentials: "include"
    });

    if (!result.ok) {
        console.log("Logout failed:", result.status);
        return;
    }

    window.location.href = "index.html";
}

// Formatting Data From JSON Reponses
function formatDate(dateString){
    return new Date(dateString).toLocaleDateString("en-KE", {
        day: "2-digit",
        month: "long",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });
}

function formatCurrency(amount){
    return new Intl.NumberFormat("en-KE", {
        style: "currency",
        currency: "KES"
    }).format(amount);
}