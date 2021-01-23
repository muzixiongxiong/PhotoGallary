import React, { useState, useEffect } from 'react'
import PhotoGallaryUser from './PhotoGallaryUser';
import axios from "axios";

export default function PhotoGallaryUserList(){
    const [userList, setUserList] = useState([])
    const [recordForEdit, setRecordForEdit] = useState(null)

    useEffect(() => {
        refreshUserList();
    }, [])

    const PhotoGallaryAPI = (url = 'https://localhost:44324/api/PhotoGallaryUsers/') => {
        return {
            fetchAll: () => axios.get(url),
            create: newRecord => axios.post(url, newRecord),
            update: (id, updatedRecord) => axios.put(url + id, updatedRecord),
            delete: id => axios.delete(url + id)
        }
    }

    function refreshUserList() {
        PhotoGallaryAPI().fetchAll()
            .then(res => {
                setUserList(res.data)
            })
            .catch(err => console.log(err))
    }

    const addOrEdit = (formData, onSuccess) => {
        if (formData.get('userID') == "0")
            PhotoGallaryAPI().create(formData)
                .then(res => {
                    onSuccess();
                    refreshUserList();
                })
                .catch(err => console.log(err))
        else
            PhotoGallaryAPI().update(formData.get('userID'), formData)
                .then(res => {
                    onSuccess();
                    refreshUserList();
                })
                .catch(err => console.log(err))

    }


    const showRecordDetails = data => {
        setRecordForEdit(data)
    }

    const onDelete = (e, id) => {
        e.stopPropagation();
        if (window.confirm('Are you sure to delete this record?'))
            PhotoGallaryAPI().delete(id)
                .then(res => refreshUserList())
                .catch(err => console.log(err))
    }

    const imageCard = data => (
        <div className="card" onClick={() => { showRecordDetails(data) }}>
            <img src={data.imageSrc} className="card-img-top rounded-circle" />
            <div className="card-body">
                <h5>{data.imageDescription}</h5>
                <button className="btn btn-light delete-button" onClick={e => onDelete(e, parseInt(data.userID))}>
                    <i className="far fa-trash-alt"></i>
                </button>
            </div>
        </div>
    )

 return(
  <div className="row">
   <div className="col-md-12">
    <div className="jumbotron jumbotron-fluid py-4">
     <div className="container text-center">
      <h1 className="display-4">Photo Gallary</h1>
     </div>
    </div>
   </div>

   <div className="col-md-4">
    <PhotoGallaryUser addOrEdit={addOrEdit} recordForEdit={recordForEdit}/>
   </div>


   <div className="col-md-8">
    <table>
        <tbody>
            {
                //tr > 3 td
                [...Array(Math.ceil(userList.length / 3))].map((e, i) =>
                    <tr key={i}>
                        <td>{imageCard(userList[3 * i])}</td>
                        <td>{userList[3 * i + 1] ? imageCard(userList[3 * i + 1]) : null}</td>
                        <td>{userList[3 * i + 2] ? imageCard(userList[3 * i + 2]) : null}</td>
                    </tr>
                )
            }
        </tbody>
    </table>
   </div>

  </div>
 )
}