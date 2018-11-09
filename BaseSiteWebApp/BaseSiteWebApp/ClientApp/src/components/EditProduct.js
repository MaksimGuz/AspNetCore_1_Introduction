import React, { Component } from 'react';
import axios from 'axios';
import { ProductData } from './FetchProducts';

export class EditProduct extends Component {
    constructor(props) {
        super(props);

        this.state = { loading: true, prodData: new ProductData(), catList: [], supList: [] };

        var prodId = this.props.match.params["prodid"];
        if (prodId === undefined)
            prodId = 0;

        axios.get('api/apicategories/')
            .then(res => {
                this.setState({ catList: res.data.categories });
            });
        axios.get('api/apisuppliers/')
            .then(res => {
                this.setState({ supList: res.data });
            });
        if (prodId > 0) {
            axios.get('api/apiproducts/' + prodId)
                .then(res => {
                    this.setState({
                        prodData: res.data,
                        loading: false
                    });
                });
        }
        else {
            this.state = { loading: false, prodData: new ProductData(), catList: [], supList: [] };
        }  

        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
        this.handleChangeCategory = this.handleChangeCategory.bind(this);
        this.handleChangeSupplier = this.handleChangeSupplier.bind(this);
        this.handleChangeDiscontinued = this.handleChangeDiscontinued.bind(this);
    }

    handleSave(event) {
        event.preventDefault();
        const formData = new FormData(event.target);
        if (this.state.prodData.productId > 0) {
            axios.put('api/apiproducts/' + this.state.prodData.productId, formData)
                .then(() => this.props.history.push('/fetchproducts'));
        }
        // POST request for Add.  
        else {
            axios.post('api/apiproducts/', formData)
            .then(() => this.props.history.push('/fetchproducts'));
        }
    } 

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchproducts");
    }

    handleChangeCategory(event) {
        let updatedData = Object.assign({}, this.state.prodData);
        updatedData.categoryId = event.target.value;
        this.setState({ prodData: updatedData });
    }

    handleChangeSupplier(event) {
        let updatedData = Object.assign({}, this.state.prodData);
        updatedData.supplierId = event.target.value;
        this.setState({ prodData: updatedData });
    }

    handleChangeDiscontinued(event) {
        let updatedData = Object.assign({}, this.state.prodData);
        updatedData.discontinued = !this.state.prodData.discontinued;
        this.setState({ prodData: updatedData });
    }
    
    renderForm() {
        let editSpecificFormContols;
        if (this.state.prodData.productId > 0) {
            editSpecificFormContols =
                <div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="unitPrice">Unit Price</label>
                        <input className="form-control valid" type="text" data-val="true" data-val-number="The field UnitPrice must be a number." data-val-range="The field UnitPrice must be between 0 and 999.99." data-val-range-max="999.99" data-val-range-min="0" id="unitPrice" name="unitPrice" defaultValue={this.state.prodData.unitPrice} aria-describedby="unitPrice-error" aria-invalid="false" />
                        <span className="text-danger field-validation-valid" data-valmsg-for="unitPrice" data-valmsg-replace="true" />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="unitsInStock">UnitsInStock</label>
                        <input className="form-control valid" type="number" id="unitsInStock" name="unitsInStock" defaultValue={this.state.prodData.unitsInStock} aria-describedby="UnitsInStock-error" aria-invalid="false" />
                        <span className="text-danger field-validation-valid" data-valmsg-for="unitsInStock" data-valmsg-replace="true" />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="unitsOnOrder">Units On Order</label>
                        <input className="form-control valid" type="number" id="unitsOnOrder" name="unitsOnOrder" defaultValue={this.state.prodData.unitsOnOrder} aria-describedby="UnitsOnOrder-error" aria-invalid="false" />
                        <span className="text-danger field-validation-valid" data-valmsg-for="unitsOnOrder" data-valmsg-replace="true" />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="reorderLevel">Reorder Level</label>
                        <input className="form-control valid" type="number" id="reorderLevel" name="reorderLevel" defaultValue={this.state.prodData.reorderLevel} aria-describedby="ReorderLevel-error" aria-invalid="false" />
                        <span className="text-danger field-validation-valid" data-valmsg-for="ReorderLevel" data-valmsg-replace="true" />
                    </div>
                </div>;
        }
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group" >
                    <input type="hidden" name="productId" value={this.state.prodData.productId} />
                </div>  
                <div className="form-group">
                    <label className="control-label" htmlFor="productName">ProductName</label>
                    <input className="form-control" type="text" data-val="true" data-val-length="The field ProductName must be a string with a maximum length of 100." data-val-length-max="100" data-val-required="The ProductName field is required." id="productName" name="productName" defaultValue={this.state.prodData.productName} />
                    <span className="text-danger field-validation-valid" data-valmsg-for="productName" data-valmsg-replace="true" />
                </div>
                <div className="form-group">
                    <label className="control-label" htmlFor="supplierId">Supplier</label>
                    <select className="form-control" data-val="true" id="supplierId" name="supplierId" aria-invalid="false" value={this.state.prodData.supplierId} onChange={this.handleChangeSupplier} >
                        {this.state.supList.map(supplier =>
                            <option key={supplier.supplierId} value={supplier.supplierId}>{supplier.companyName}</option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    <label className="control-label" htmlFor="categoryId">Category</label>
                    <select className="form-control" data-val="true" id="categoryId" name="categoryId" aria-invalid="false" value={this.state.prodData.categoryId} onChange={this.handleChangeCategory} >
                        {this.state.catList.map(category =>
                            <option key={category.categoryId} value={category.categoryId}>{category.categoryName}</option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    <label className="control-label" htmlFor="quantityPerUnit">Quantity Per Unit</label>
                    <input className="form-control valid" type="text" id="quantityPerUnit" name="quantityPerUnit" aria-invalid="false" defaultValue={this.state.prodData.quantityPerUnit} />
                    <span className="text-danger field-validation-valid" data-valmsg-for="quantityPerUnit" data-valmsg-replace="true" />
                </div>                
                {editSpecificFormContols}
                <div className="form-group">
                    <div className="checkbox">
                        <label>
                            <input type="checkbox" data-val="true" id="discontinued" name="discontinued" defaultChecked={this.state.prodData.discontinued} value={this.state.prodData.discontinued} onChange={this.handleChangeDiscontinued} /> Discontinued
                        </label>
                    </div>
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
        let header = this.state.prodData.productId > 0 ? 'Edit' : 'Create';
        return (
            <div>
                <h2>{header}</h2>
                <h2>Product</h2>
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