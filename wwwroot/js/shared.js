// Logout Functionality
document
    .getElementById("logoutBtn")
    .addEventListener("click", logout);
    
async function logout(){
    await fetch("https://polisync-api.onrender.com/api/auth/login", {
        method: "POST",
        credentials: "include"
    });

    window.location.href = "login.html";
}