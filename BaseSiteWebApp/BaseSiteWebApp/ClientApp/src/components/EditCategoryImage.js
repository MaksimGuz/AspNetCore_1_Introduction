import React, { Component } from 'react';
import axios from 'axios';

export class EditCategoryImage extends Component {
    constructor(props) {
        super(props);

        this.state = { loading: true, catId: this.props.match.params["catid"], name: '', file: null };

        this.onFormSubmit = this.onFormSubmit.bind(this);
        this.onChangeFile = this.onChangeFile.bind(this);
        this.onChangeName = this.onChangeName.bind(this);
        this.handleCancel = this.handleCancel.bind(this); 
    }

    componentDidMount() {
        axios.get('api/apicategories/' + this.state.catId)
            .then(res => {
                this.setState({
                    name: res.data.categoryName,
                    loading: false
                });
            });
    }

    onFormSubmit(e) {
        e.preventDefault(); // Stop form submit
        const formData = new FormData();
        formData.append('categoryId', this.state.catId);
        formData.append('categoryName', this.state.name);
        formData.append('pictureFile', this.state.file);
        return axios.put(
            'api/apicategories/' + this.state.catId,
            formData,
            { headers: { 'content-type': 'multipart/form-data' } }
        )
            .then(() => this.props.history.push('/fetchcategories'));
    }

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchcategories");
    } 

    onChangeFile(e) {
        this.setState({ file: e.target.files[0] });
    }

    onChangeName(e) {
        this.setState({ name: e.target.value });
    }
    renderForm() {
        return (
            <form onSubmit={this.onFormSubmit} encType="multipart/form-data" >
                <div className="form-group">
                    <label className="control-label">Category Name</label>
                    <input className="form-control" type="text" data-val="true" data-val-required="The CategoryName field is required." id="categoryName" name="categoryName" value={this.state.name} onChange={this.onChangeName} />
                    <span className="text-danger field-validation-valid" data-valmsg-for="categoryName" data-valmsg-replace="true" />
                </div>
                <div className="form-group">
                    <label className="control-label">Picture File</label>
                    <input type="file" id="pictureFile" name="pictureFile" onChange={this.onChangeFile} />
                </div>
                <div className="form-group">
                    <input type="submit" value="Save" className="btn btn-primary" />                    
                </div>
            </form>
        );
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> :
            this.renderForm();
        return (
            <div>
                <h2>Category Image</h2>
                <div className="row">
                    <div className="col-md-4">
                        {contents}
                    </div>
                </div>
                <div>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div>
            </div>
        );
    }
}