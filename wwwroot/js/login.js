// Login Functionality

const loginForm = document.getElementById("loginForm");

loginForm.addEventListener("submit", login);

async function login(e){
    e.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const response = await fetch("https://polisync-api.onrender.com/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            username,
            password
        })
    });

    if (!response.ok){
        alert("Invalid username or password.");
        return;
    }


    const user = await response.json();

    if (user.role === "Customer"){
        window.location.href = "customer.html";
        return;
    }

    if (user.role === "Administrator"){
        window.location.href = "admin.html";
        return;
    }
}