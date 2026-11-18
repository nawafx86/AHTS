
const UI =
{
    btnAdd: document.getElementById("btn-add"),
    table:
    {
        tableBody: document.getElementById("tableBody"),
        pageNum: document.getElementById("spnPage"),
        pageNext: document.getElementById("btnPageNext"),
        pagePrev: document.getElementById("btnPagePrev")
    },
    summary:
    {
        allAssetsCount: document.getElementById("AllAssetsCountNum"),
        allAssginedStatusCount: document.getElementById("AssignStatusNum"),
        allAvailableStatusCount: document.getElementById("AvailabeStatusNum"),
    },
    form:
    {
        form: document.getElementById("frmDialog"),
        btnAddNewAsset: document.getElementById("frmBtnConfirm"),
        btnCancel: document.getElementById("frmBtnCancel"),

    },
    update:
    {
        form: document.getElementById("frmDialogUpdate"),
        btnUpdateAsset: document.getElementById("frmUpdateBtnConfirm"),
        btnCancel: document.getElementById("frmUpdateBtnCancel"),
    }
}

async function getSummaryCounts()
{
    try {
        const endpoints = [
            'https://localhost:7090/AssetController/getAllAssetCount',
            'https://localhost:7090/AssetController/getAllStatusCount'
        ]
        const response = endpoints.map(async (url) => 
        {
            const result = await fetch(url);
            if (!result.ok) {
                throw new Error("Result did not return ok");
            }
            return await result.json();
        })

        const responses = await Promise.allSettled(response);
        const failed = responses.some(result => result.status === 'rejected');
        if (failed) {
            throw new Error("allAssets did not return OK");
        }
        
        const data = responses.map(result => result.value);
        return data;
    } catch (error) {
        console.error("Error fetching assets:", error);
        return [];
    }
}
 async function getAllAssets()
{
    try {
        const page = parseInt(UI.table.pageNum.textContent);
        const response = await fetch(`https://localhost:7090/AssetController/getAllAssets?pageNumber=${page}&pageSize=5`);
        
        if (!response.ok) {
            throw new Error("allAssets did not return OK");
        }
        
        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error fetching assets:", error);
        return [];
    }
}

async function renderPageOnLoad()
{
   const assetsJson = await getAllAssets();
      let assets = [];
    const rawData = Array.isArray(assetsJson) 
       ? assetsJson 
       : (assetsJson.array || assetsJson.values || assetsJson.$values || []);   
    rawData.forEach(element => {
    assets.push({
    AssetID: element.assetId,
    SerialNumber: element.serialNumber,
    DeviceType: element.deviceType,
    Status: element.status,
    EmployeeID: element.assignedToEmployeeId ? element.assignedToEmployeeId : "null"
   })
   });

   let tableBody = UI.table.tableBody;
   tableBody.innerHTML = "";

   assets.forEach(e => 
   {
    let tableRow = document.createElement("tr");

    let AssetID = document.createElement("td");
    AssetID.textContent = e.AssetID;
    let SerialNumber = document.createElement("td");
    SerialNumber.textContent = e.SerialNumber;
    let DeviceType = document.createElement("td");
    DeviceType.textContent = e.DeviceType;
    let Status = document.createElement("td");
    Status.textContent = e.Status;
    if(Status.textContent === "Available")
    {
        Status.classList.add("statusAvailable");
    }
    else if (Status.textContent === "Assigned")
    {
        Status.classList.add("statusAssigned");
    }
    let EmployeeID = document.createElement("td");
    EmployeeID.textContent = e.EmployeeID;

    let UpdateAsset = document.createElement("td");
    let ButtonUpdate = document.createElement("button");
    ButtonUpdate.textContent = "Update";
    ButtonUpdate.dataset.id = AssetID.textContent;
    ButtonUpdate.classList.add('update-asset-btn');

    UpdateAsset.append(ButtonUpdate);

    tableRow.append(AssetID);
    tableRow.append(SerialNumber);
    tableRow.append(DeviceType);
    tableRow.append(Status);
    tableRow.append(EmployeeID);
    tableRow.append(UpdateAsset);


    tableBody.append(tableRow);
   }


   )
      const SummaryCounts = await getSummaryCounts();
      const SummaryCountsObject = SummaryCounts ? 
      {
        allAssignedStatusCount: SummaryCounts[1].Assigned,
        allAvailableStatusCount: SummaryCounts[1].Available,
        allAssetsCount: SummaryCounts[0]
      } : {};
      UI.summary.allAssetsCount.textContent = SummaryCountsObject?.allAssetsCount;
      UI.summary.allAssginedStatusCount.textContent = SummaryCountsObject?.allAssignedStatusCount;
      UI.summary.allAvailableStatusCount.textContent = SummaryCountsObject?.allAvailableStatusCount;

   
}


async function getAlldevices()
{
    try 
    {
    const DeviceResponse = await fetch('https://localhost:7090/AssetController/getAllDevices');
    
        if(!DeviceResponse.ok)
        {
            throw new Error("Error While Fetching");
        }
        const data = await DeviceResponse.json();
        return data;
    
    } 
    catch (error) 
    {
        console.error(`Error Happened while Fetching the ${error}`);
        return [];
    }
}

async function getAllEmployeeNamesAndIds()
{
    try 
    {
    const EmployeesResponse = await fetch('https://localhost:7090/employeeApiController/getAllEmployeesNameAndID');
    
        if(!EmployeesResponse.ok)
        {
            throw new Error("Error While Fetching");
        }
        const data = await EmployeesResponse.json();
        return data;
    
    } 
    catch (error) 
    {
        console.error(`Error Happened while Fetching the ${error}`);
        return {};
    }
}

async function onButtonAddClick()
{
    const dialog = document.getElementById("AddAssetDialog");
     dialog.showModal();
    const Devices = await getAlldevices();
    const SelectElement = document.getElementById("devices-types");
    SelectElement.innerHTML = "";
    Devices.forEach(element =>
    {
        let option = document.createElement('option');
        option.value = element.toLowerCase();
        option.textContent = element;

        SelectElement.append(option);
    }
    )

    const Employees = await getAllEmployeeNamesAndIds();
    const SelectElementEmployee = document.getElementById("employees-select");
    SelectElementEmployee.innerHTML = "";
    for (let id in Employees)
    {
    let option = document.createElement('option');
    option.value = id;
    option.textContent = Employees[id];
    SelectElementEmployee.append(option);
    }
    
}

async function AddNewAsset(asset)
{
    try
    {

        const AddAssetResponse = await fetch("https://localhost:7090/AssetController/addAsset", 
        {
            method: 'POST',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(asset)
        })

        if(!AddAssetResponse.ok)
        {
            throw new Error("Error Happend While Adding new Asset");
        }

        const result = response.json();
        console.log('Success:', result);
        return result;
    }
    catch (error)
    {
        console.error("Error: " + error);
    }
}

async function onFrmBtnAddAssetClick(event)
{
    event.preventDefault();
    const form = event.target;

  if (!form.checkValidity()) {
    form.reportValidity();
    return;
    }

        const Employee = document.getElementById("employees-select");
        const Device = document.getElementById("devices-types");
        const SerialNumber = document.getElementById("serial-number");
        const Status = "Assigned";

    const newAsset = {
        assignedToEmployeeId: Employee.value,
        deviceType: Device.value,
        serialNumber: SerialNumber.value,
        status: Status
    }
    await AddNewAsset(newAsset);
    await renderPageOnLoad();

    const dialog = document.getElementById("AddAssetDialog");
    dialog.close();
}

async function onButtonUpdateSubmit(data)
{
    try
    {
        const asset = await fetch(`https://localhost:7090/${parseInt(data)}/getAsset`);
        if(!asset.ok)
        {
            throw new Error("Error Has Happened While Looking for the Data");
        }
        
        const fetchedData = await asset.json();
        if(fetchedData.assetId === -1) return {};

        UpdateAsset(fetchedData);
    }
    catch(error)
    {
        console.log(error);
    }
}

async function UpdateAsset(fetchedData)
{
    try
    {
        let Asset =
         {
            AssetId: fetchedData.assetId,
            Name: fetchedData.name,
            SerialNumber: fetchedData.serialNumber,
            DeviceType: fetchedData.deviceType,
            Status: fetchedData.status,
            AssignedToEmployeeId: fetchedData.assignedToEmployeeId
        }

     const checkbox = document.getElementById("isNull");
        let updatedAsset;
        if(checkbox.checked)
        {
            updatedAsset = {
                assetId: fetchedData.assetId,
                status: "Available",
                EmployeeID: null
            }
        }
        else
        {
        const EmployeeSelectElement = document.getElementById("updateDialog-employees-select");
         updatedAsset = {
                assetId: fetchedData.assetId,
                status: "Assigned",
                EmployeeID: EmployeeSelectElement.value
            }
        }
        const UpdateAsset = await fetch('https://localhost:7090/AssetController/changeEmployeeIdAndStatus',
            {
                method: 'PATCH',
                headers:
                {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedAsset)
            });
            if(!UpdateAsset.ok)
            {
                throw new Error("Update Error");
            }
            await UpdateAsset.json();
        }
        catch(error)
        {
            console.log(error);
        }
}

async function renderUpdate()
{
    const chkIsNull = document.getElementById("isNull");
    const employeesList = document.getElementById("updateDialog-employees-select");
    const dialog = document.getElementById("UpdateAssetDialog");
    dialog.showModal();
    const employees = await getAllEmployeeNamesAndIds();
    employeesList.innerHTML = "";
     for (let id in employees)
    {
    let option = document.createElement('option');
    option.value = id;
    option.textContent = employees[id];
    employeesList.append(option);
    }

     chkIsNull.addEventListener('change', () => 
    {
        employeesList.disabled = chkIsNull.checked;
    })
}

document.addEventListener('DOMContentLoaded',  renderPageOnLoad);
UI.btnAdd.addEventListener('click',  onButtonAddClick);
UI.form.form.addEventListener('submit', onFrmBtnAddAssetClick);
UI.form.btnCancel.addEventListener('click', async (e) =>
    {
        e.preventDefault();
        const dialog = document.getElementById("AddAssetDialog");
        dialog.close();
    }
);

UI.update.btnCancel.addEventListener('click', async (e) =>
    {
        e.preventDefault();
        const dialog = document.getElementById("UpdateAssetDialog");
        dialog.close();
    }
);
UI.table.pageNext.addEventListener('click', async () =>
{
    const currentPage = parseInt(UI.table.pageNum.textContent);
    const tableBody = UI.table.tableBody;
    if (!tableBody.textContent)
    {
        return;
    }

    UI.table.pageNum.textContent = currentPage + 1;
    await renderPageOnLoad();
});

UI.table.pagePrev.addEventListener('click', async () =>
{
    const currentPage = parseInt(UI.table.pageNum.textContent);
    
    if (currentPage === 1) 
    {
        return;
    }
    UI.table.pageNum.textContent = currentPage - 1;
    await renderPageOnLoad();
});


UI.table.tableBody.addEventListener('click', async function(event) {
  const target = event.target;
  
  if (target.matches('.update-asset-btn')) {
    const dialog = document.getElementById("UpdateAssetDialog");
    dialog.value = target.dataset.id;
    await renderUpdate();
  }
});

UI.update.form.addEventListener('submit', async (event) =>
{
    event.preventDefault();
    const dialog = document.getElementById("UpdateAssetDialog");
    await onButtonUpdateSubmit(dialog.value);
    dialog.close();
    setTimeout(async () =>
        {
         await renderPageOnLoad();
        }, 500);
});