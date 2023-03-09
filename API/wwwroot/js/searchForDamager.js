
async function retrieveDamagers() {
    const damageInfo = document.getElementById("Damager")
    fetch('https://gateway.marvel.com:443/v1/public/characters?ts=1&nameStartsWith=' + damageInfo.value + '&apikey=8ecae25f58b2e8b15e9eaded61953912').then((res) => res.json()).then(data => console.log(data))
}