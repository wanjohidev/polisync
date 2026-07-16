// Login Functionality

const loginForm = document.getElementById("loginForm");

loginForm.addEventListener("submit", login);

async function login(e){
    e.preventDefault();

    const email = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const response = await fetch("https://polisync-api.onrender.com/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            email,
            password
        })
    });

    const body = await response.text();
    console.log(response.status);
    console.log(body);
    
    if (!response.ok){
        alert("Invalid username or password.");
        return;
    }

    const result = await response.json();

    if (result.role === "Customer"){
        window.location.href = "customer.html";
        return;
    }

    if (result.role === "Administrator"){
        window.location.href = "admin.html";
        return;
    }
}