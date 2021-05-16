using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.State
{
    public class InventoryState
    {
        public static GameObject[] Inventory { get; private set; } = new GameObject[10];
    }
}
