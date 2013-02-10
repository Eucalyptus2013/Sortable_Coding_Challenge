#Sortable Coding Challenge

This is a solution for the Sortable.com's [coding challenge](http://sortable.com/blog/coding-challenge/).

##How to use the program
1. Download the zip
2. In the Input folder, put products.txt and listings.txt
3. Run the run.bat
4. results.txt is in the Output folder

##How the program works
###Step 1: Getting the data
* Data is extracted from products.txt and listings.txt by deserializing JSON data. 
* For the listings.txt, new Listing objects are created and added to a list.
* For the products.txt, we use a tree to store all the products. The class Product has five class variables (product_name, manufacturer, family, model, announced_date). We use the class function LevelsOrder() to create the hierarchy of class variables.


```csharp
public string[] LevelsOrder()
        {
            return new string[] { "", manufacturer, family, model, product_name };
        }
```
There are 5 levels:   
0: Nothing (used for the head of the list of products)  
1: Manufacturer  
2: Family  
3: Model  
4: Product_name

When a product is added, we start by the head and look recursively at each level for a string match (using the string array from the function LevelsOrder) and if not we create a new node.  
For example, if we want the Family string of the product added, we look to LevelsOrder()[2].

###Step 2: Getting the matches
> The task is to match each listing to the correct product. Precision is critical. We much prefer missed matches (lower recall) over incorrect matches, so try hard to avoid false positives.

We're looking recursively for exact string match at each level. If we are at the last level, it means all the previous levels have matched and we can add this product_name to our list of matches.

###Step 3: Ranking the matches
> A single price listing may match at most one product. 

If we have more than one match, we need to rank the matches and return the top rank. At this point, there's an infinity of ranking algorithms to choose and no one will be better than the others. My ranking algorithm is an alphabetical sorting.

###Step 4: Writing the results
* For each listing, we have a product_name or nothing.
* We use a dictionary with product_name as the key and a list of listings as the value
* We iterate over the dictionary, serializing Result objects.
* Result object has a product_name and a list of listings