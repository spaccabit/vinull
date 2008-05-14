using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;
using Microsoft.Data.Web;

public class AWDataService : WebDataService<Model.AdventureWorksEntities> {
    // This method is called once during service initialization to allow
    // service-specific policies to be set
    public static void InitializeService(IWebDataServiceConfiguration config) {
        
        config.SetResourceContainerAccessRule("*",
                            ResourceContainerRights.All);
        
        // TODO: set rules to indicate which entity sets and service operations are
        // visible, updatable, etc.
        // (for testing purposes use "*" to indicate all entity sets/service
        // operations, but that option should NOT be used in production systems)

        // Example for entity sets (this example uses "AllRead" which allows reads but not writes)
        // config.SetResourceContainerAccessRule("MyEntityset", ResourceContainerRights.AllRead);

        // Example for service operations
        // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
    }

    // Query interceptors, change interceptors and service operations go here
}
