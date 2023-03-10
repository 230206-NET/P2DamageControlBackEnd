function pushTicket(event) {
    event.preventDefault()
    var amount = parseFloat(document.getElementById("Amount").value)
    var damager = document.getElementById("Damager").value
    var description = document.getElementById("Description").value
    console.log(Damager)
    const reqBody = {
        Amount: amount,
        DamagerId: damager,
        Description: description
    }
    fetch("http://localhost:5025/TicketForm/SubmitClaim", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(reqBody)
    }).then((res) => {console.log(res); res.json()}).then(data => console.log(data)).catch(error => console.error(error))
    setTimeout(function() {
        window.location.href = "http://localhost:5025/ViewAllTickets";
      }, 3000);
}