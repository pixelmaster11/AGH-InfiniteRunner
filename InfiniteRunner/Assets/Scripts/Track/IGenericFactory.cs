using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericFactory <T>
{

    void ManufactureProduct(int productQuantity, ref T[] products, Transform productParent);

    T DeliverProduct(ref T[] products);
	
}
