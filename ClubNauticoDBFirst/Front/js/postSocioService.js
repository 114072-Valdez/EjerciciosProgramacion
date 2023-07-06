function guardarSocio(){
    let nombre = document.getElementById('txtNombre')
    let apellido = document.getElementById('txtApellido')
    let telefono = document.getElementById('txtTelefono')
  
    if(nombre.value === ""){
        alert("El nombre es obligatorio")
        return false
    }
  
    if(apellido.value === ""){
        alert("El apellido es obligatorio")
        return false
    }
  
    if(telefono.value === ""){
        alert("El telefono es obligatorio")
        return false
    }
  
    const url = 'https://localhost:7205/api/Socio'
  
    //contruyo al request
    const request = {
        "nombre": nombre.value,
        "apellido": apellido.value,
        "telefono": telefono.value
    }
  
    fetch(url,{
        body: JSON.stringify(request),
        method: 'post',
        headers: {
            'Content-Type':'application/json'
        }
    })
    .then(respuesta => respuesta.json())
    .then(respuesta => {
        if(respuesta.ok){
            alert("Socio agregado con éxito")
            localStorage.setItem("datoAMostrar", respuesta.listSocios[0].nombre)
            window.location.replace('index.html');

        }
        else{
            alert(respuesta.mensajeError)
        }
    })
    .catch(err => alert("ERROR: " + err))
  }