const api = "/api";
// Login Functionality

const loginForm = document.getElementById("loginForm");

loginForm.addEventListener("submit", login);

async function login(e){
    e.preventDefault();

    const email = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const response = await fetch(`${api}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            email,
            password
        })
    });
    
    if (!response.ok){
        alert("Invalid username or password.");
        return;
    }

    // what does the AuthController login endpoint return - Message & Role
    const result = await response.json();

    console.log(result);

    if (result.role === "Customer"){
        window.location.href = "customer.html";
        return;
    }

    if (result.role === "Administrator"){
        window.location.href = "admin.html";
        return;
    }
}