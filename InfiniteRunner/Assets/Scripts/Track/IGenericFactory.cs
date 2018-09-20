using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a generic interface for creating a factory of any type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericFactory <T>
{
    /// <summary>
    /// Create a product pool 
    /// </summary>
    /// <param name="productQuantity">Manufactures products of required quantity</param>
    /// <param name="products">What products to manufacture</param>
    /// <param name="productParent">Where to keep these products in the scene</param>
    void ManufactureProduct(int productQuantity, ref T[] products, Transform productParent);

    /// <summary>
    /// Return a product
    /// </summary>
    /// <param name="products">If no products in the pool as per requirement, then create additional products using these products</param>
    /// <param name="optional">An optional integer value to use as per needs</param>
    /// <returns></returns>
    T DeliverProduct(ref T[] products, int optional = 0);
	
}
