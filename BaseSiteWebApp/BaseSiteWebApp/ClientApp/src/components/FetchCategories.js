import React, { Component } from 'react';

export class FetchCategories extends Component {
    displayName = FetchCategories.name

    constructor(props) {
        super(props);
        this.state = { catlist: [], loading: true };

        fetch('api/categories')
            .then(response => response.json())
            .then(data => {
                this.setState({ catlist: data.categories, loading: false });
            });

        // This binding is necessary to make "this" work in the callback
        this.handleEdit = this.handleEdit.bind(this);  
    }

    handleEdit(id) {
        this.props.history.push("/editcategoryimage/" + id);
    }

    renderCategoriesTable(catlist) {
        return <table className="table">
            <thead>
                <tr>
                    <th />
                    <th>CategoryName</th>
                    <th>Description</th>
                    <th />
                    <th />
                </tr>
            </thead>
            <tbody>
                {catlist.map(cat =>
                    <tr key={cat.categoryId}>
                        <td />
                        <td>{cat.categoryName}</td>
                        <td>{cat.description}</td>
                        <td><a target="_blank" href={'/api/categoryimages/' + cat.categoryId}>Image</a></td>
                        <td><a className="action" onClick={(id) => this.handleEdit(cat.categoryId)}>Edit</a> </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> :
            this.renderCategoriesTable(this.state.catlist);
        return (
            <div>
                <h2>Categories</h2>
                {contents}
            </div>
        );
    }
}