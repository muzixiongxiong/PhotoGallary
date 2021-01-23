import React,{useState,useEffect} from 'react'

const defaultIconSrc = '/img/default-user-icon.jpg';

const initialFieldValues = {
 userID:0,
 imageDescription:'',
 imageName:'',
 imageSrc:defaultIconSrc,
 imageFile:null
}

export default function PhotoGallaryUser(props){
 const { addOrEdit, recordForEdit } = props;
 const [values,setValues] = useState(initialFieldValues);
 const [errors,setErrors] = useState({});

useEffect(() => {
 if (recordForEdit != null)
   setValues(recordForEdit);
  }, [recordForEdit])

 const handleInputChange = e=> {
  const {name,value} = e.target;
  setValues({
   ...values,
   [name]:value
  })
 }

 const previewImage = e => {
  if(e.target.files && e.target.files[0]){
   let imageFile = e.target.files[0];
   const reader = new FileReader();
   reader.onload = x=> {
    setValues({
     ...values,
     imageFile,
     imageSrc: x.target.result
    })
   }
   reader.readAsDataURL(imageFile);
  }
  else{
   setValues({
     ...values,
     imageFile:null,
     imageSrc: defaultIconSrc
    })
  }
 }

const validate = () => {
 let temp = {}
 temp.imageDescription = values.imageDescription ==""?false:true;
 temp.imageSrc = values.imageSrc == defaultIconSrc?false:true;
 setErrors(temp)
 return Object.values(temp).every(x=> x== true)
}

const resetForm = () =>{
 setValues(initialFieldValues);
 document.getElementById("image-uploader").value = null;
 setErrors({})
}

 const handleFormSubmit = e =>{
  e.preventDefault();
  if(validate()){
   const formData = new FormData();
   formData.append('userID',values.userID);
   formData.append('imageDescription',values.imageDescription);
   formData.append('imageName',values.imageName);
   formData.append('imageFile',values.imageFile);
   addOrEdit(formData,resetForm);
  }

 }

 const applyErrorClass = field => ((field in errors && errors[field]==false)?' invalid-field':'')

 return(
  <>
   <div className="container text-center">
    <p className="lead">Post A Photo</p>
   </div>
   <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
    <div className="card">
     <img src={values.imageSrc} className="card-img-top"/>
     <div className="card-body">
      <div className="form-group">
       <input type="file" accept="image/*" className={"form-control-file"+applyErrorClass('imageSrc')} onChange={previewImage} id="image-uploader"/>
      </div>
      <div className="form-group">
       <input className={"form-control-file"+applyErrorClass('imageDescription')} placeholder="Description" name="imageDescription" value={values.imageDescription} onChange={handleInputChange}/>
      </div>
      <div className="form-group text-center">
       <button type="submit" className="btn btn-light">Submit</button>
      </div>
     </div>
    </div>
   </form>
  </>
 )
}