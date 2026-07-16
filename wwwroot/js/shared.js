const api = "/api";
// Logout Functionality
document
    .getElementById("logoutBtn")
    .addEventListener("click", logout);
    
async function logout(){
    await fetch(`${api}/auth/login`, {
        method: "POST",
        credentials: "include"
    });

    window.location.href = "login.html";
}