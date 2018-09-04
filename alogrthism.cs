using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Memory_Allocation
{


    class alogrthism
    {

        List<block> mylist;
        int memorysize;

        public alogrthism()
        {
            mylist = new List<block>();
        }
        public alogrthism(int memsize)
        {
            mylist = new List<block>();
            memorysize = memsize;
        }
        public List<block> Enter_the_init_list(List<startlist> listOfstartlist)
        {
            input(listOfstartlist);
            List<block> mylist2 = new List<block>(mylist);
            return mylist2;
        }


        public void check_the_name(ref pro processes)
        {
            int j = 0;
            int flag = 0;
            int first = 0;
        lo:
            for (int i = 0; i < mylist.Count; i++)
            {
                if (mylist[i].name == processes.name)
                    flag = 1;

            }
            j++;
            if ((first == 0) && (flag == 1))
            {
                first = 1;
                flag = 0;
                processes.name += "-copy(";
                processes.name += j.ToString();
                processes.name += ")";
                goto lo;
            }
            else if (flag == 1)
            {
                flag = 0;
                int postion;
                postion = processes.name.LastIndexOf("(");
                processes.name = processes.name.Remove(postion, processes.name.Count() - postion);
                processes.name += "(";
                processes.name += j.ToString();
                processes.name += ")";
                goto lo;
            }



        }

        public bool first_fit(pro processes, ref List<block> printlist)
        {


            //at first clear the list to be returned 
            printlist.Clear();

            check_the_name(ref processes);
            // no need to sort as it is first fit 
            block temp;
            for (int i = 0; i < mylist.Count; i++)
            {
                //traverse untill u find the place and break that block and put in it
                if (mylist[i].type == 0)
                {
                    if (mylist[i].blocksize > processes.size)
                    {
                        temp = mylist[i];
                        int size_remainig = temp.blocksize - processes.size;
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        temp.blocksize = processes.size;
                        temp.endingaddress = processes.size + temp.startingaddress;
                        mylist.Insert(i, temp);
                        temp.startingaddress = temp.endingaddress;
                        temp.blocksize = size_remainig;
                        temp.name = "HOLE";
                        temp.type = 0;
                        temp.endingaddress = size_remainig + temp.startingaddress;
                        mylist.Insert(i + 1, temp);
                        // copy from your list to the other


                        printlist = new List<block>(mylist);
                        // print();
                        return true;
                    }
                    if (mylist[i].blocksize == processes.size)
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        mylist.Insert(i, temp);
                        // copy from your list to the other


                        printlist = new List<block>(mylist);
                        //              print();
                        return true;
                    }
                }

            }

            // copy from your list to the other
            //printlist = mylist.ToList();
            //   copy_list(ref printlist);
            printlist = new List<block>(mylist);
            //      print();
            return false;

        }


        private void copy_list(ref List<block> printlist)
        {
            block item;
            for (int i = 0; i < mylist.Count; i++)
            {
                item = mylist[i];
                printlist.Add(item);

            }



        }
        public bool best_fit(pro processes, ref List<block> printlist)
        {
            //at first clear the list to be returned 
            printlist.Clear();
            check_the_name(ref processes);

            //sort 
            mylist.Sort((x, y) => x.blocksize.CompareTo(y.blocksize));
            // look for a place for it
            block temp;
            for (int i = 0; i < mylist.Count; i++)
            {
                //traverse untill u find the place and break that block and put in it
                if (mylist[i].type == 0)
                {
                    if (mylist[i].blocksize > processes.size)
                    {
                        temp = mylist[i];
                        int size_remainig = temp.blocksize - processes.size;
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        temp.blocksize = processes.size;
                        temp.endingaddress = processes.size + temp.startingaddress;
                        mylist.Insert(i, temp);
                        temp.startingaddress = temp.endingaddress;
                        temp.blocksize = size_remainig;
                        temp.name = "HOLE";
                        temp.type = 0;
                        temp.endingaddress = size_remainig + temp.startingaddress;
                        mylist.Insert(i + 1, temp);
                        // sort your own list back
                        mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));
                        // copy from your list to the other

                        printlist = new List<block>(mylist);
                        return true;
                    }

                    if (mylist[i].blocksize == processes.size)
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        mylist.Insert(i, temp);
                        // sort your own list back
                        mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));
                        // copy from your list to the other

                        printlist = new List<block>(mylist);
                        return true;
                    }
                }
                //traverse untill u find the place and break that block
                // at which return true and update the list
                /*
                 // sort your own list back
                 // copy from your list to the other
                 //return True ;
                */






            }

            // sort your own list back
            mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));
            // copy from your list to the other

            printlist = new List<block>(mylist);
            return false;
        }
        public bool worst_fit(pro processes, ref List<block> printlist)
        {
            //at first clear the list to be returned 
            printlist.Clear();
            check_the_name(ref processes);

            //sort 
            mylist.Sort((x, y) => x.blocksize.CompareTo(y.blocksize));
            //reverse
            mylist.Reverse();//to get bigger first
            block temp;
            for (int i = 0; i < mylist.Count; i++)
            {
                //traverse untill u find the place and break that block and put in it
                if (mylist[i].type == 0)
                {
                    if (mylist[i].blocksize > processes.size)
                    {
                        temp = mylist[i];
                        int size_remainig = temp.blocksize - processes.size;
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        temp.blocksize = processes.size;
                        temp.endingaddress = processes.size + temp.startingaddress;
                        mylist.Insert(i, temp);
                        temp.startingaddress = temp.endingaddress;
                        temp.blocksize = size_remainig;
                        temp.name = "HOLE";
                        temp.type = 0;
                        temp.endingaddress = size_remainig + temp.startingaddress;
                        mylist.Insert(i + 1, temp);
                        // sort your own list back
                        mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));

                        // copy from your list to the other

                        printlist = new List<block>(mylist);
                        return true;
                    }

                    if (mylist[i].blocksize == processes.size)
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);
                        temp.name = processes.name;
                        temp.type = 1;
                        mylist.Insert(i, temp);
                        // sort your own list back
                        mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));
                        // copy from your list to the other

                        printlist = new List<block>(mylist);
                        return true;
                    }
                }
                //traverse untill u find the place and break that block
                // at which return true and update the list
                /*
                 // sort your own list back
                 // copy from your list to the other
                 //return True ;
                */

            }
            // sort your own list back
            mylist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));
            // copy from your list to the other

            printlist = new List<block>(mylist);
            return false;
        }

        public void remove_by_starting_address(int ad, ref List<block> printlist)
        {
            printlist.Clear();
            block temp, temp2;
            for (int i = 0; i < mylist.Count; i++)
            {
                if (ad == mylist[i].startingaddress)
                {
                    if (i == 0)
                    {
                        if (mylist[i + 1].type == 0)
                        {
                            temp = mylist[i];
                            mylist.RemoveAt(i);

                            temp2 = mylist[i];
                            mylist.RemoveAt(i);

                            temp.endingaddress = temp2.endingaddress;
                            temp.blocksize += temp2.blocksize;
                            temp.name = "HOLE";
                            temp.type = 0;
                            mylist.Insert(i, temp);

                            printlist = new List<block>(mylist);
                            return;
                        }
                        else
                        {
                            temp = mylist[i];
                            mylist.RemoveAt(i);
                            temp.type = 0;
                            temp.name = "HOLE";
                            mylist.Insert(i, temp);

                            printlist = new List<block>(mylist);
                            return;
                        }



                    }

                    if (i == mylist.Count-1)
                    {
                        if (mylist[i - 1].type == 0)
                        {
                            temp = mylist[i];
                            mylist.RemoveAt(i);
                            temp2 = mylist[i - 1];
                            mylist.RemoveAt(i - 1);
                            temp2.endingaddress = temp.endingaddress;
                            temp2.blocksize += temp.blocksize;
                            temp2.name = "HOLE";
                            temp2.type = 0;
                            mylist.Insert(i - 1, temp2);

                            printlist = new List<block>(mylist);
                            return;

                        }
                        else
                        {
                            temp = mylist[i];
                            mylist.RemoveAt(i);
                            temp.type = 0;
                            temp.name = "HOLE";
                            mylist.Insert(i, temp);

                            printlist = new List<block>(mylist);
                            return;
                        }




                    }








                    if ((mylist[i + 1].type == 0) && (mylist[i - 1].type == 0))
                    {
                        block temp3;
                        temp = mylist[i - 1];
                        temp2 = mylist[i];
                        temp3 = mylist[i + 1];

                        mylist.RemoveAt(i - 1);
                        mylist.RemoveAt(i - 1);
                        mylist.RemoveAt(i - 1);

                        temp.endingaddress = temp3.endingaddress;
                        temp.blocksize += temp2.blocksize + temp3.blocksize;
                        temp.name = "HOLE";
                        temp.type = 0;
                        mylist.Insert(i - 1, temp);

                        printlist = new List<block>(mylist);
                        return;

                    }
                    else if (mylist[i - 1].type == 0)
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);
                        temp2 = mylist[i - 1];
                        mylist.RemoveAt(i - 1);
                        temp2.endingaddress = temp.endingaddress;
                        temp2.blocksize += temp.blocksize;
                        temp2.name = "HOLE";
                        temp2.type = 0;
                        mylist.Insert(i - 1, temp2);

                        printlist = new List<block>(mylist);
                        return;

                    }

                    else if (mylist[i + 1].type == 0)
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);

                        temp2 = mylist[i];
                        mylist.RemoveAt(i);

                        temp.endingaddress = temp2.endingaddress;
                        temp.blocksize += temp2.blocksize;
                        temp.name = "HOLE";
                        temp.type = 0;
                        mylist.Insert(i, temp);

                        printlist = new List<block>(mylist);
                        return;
                    }
                    else
                    {
                        temp = mylist[i];
                        mylist.RemoveAt(i);
                        temp.type = 0;
                        temp.name = "HOLE";
                        mylist.Insert(i, temp);

                        printlist = new List<block>(mylist);
                        return;
                    }
                }
            }
        }

        public void compact(ref List<block> printlist)
        {
            printlist.Clear();
            int timer = 0;
            block temp;
            for (int i = 0; i < mylist.Count; i++)
            {
                if (mylist[i].type == 1)
                {
                    temp = mylist[i];
                    temp.startingaddress = timer;
                    temp.endingaddress = timer + temp.blocksize;
                    printlist.Add(temp);
                    timer = temp.endingaddress;
                }
            }
            temp.type = 0;
            temp.name = "HOLE";
            temp.blocksize = memorysize - timer;
            temp.endingaddress = memorysize;
            temp.startingaddress = timer;
            if (temp.blocksize != 0)
                printlist.Add(temp);

            mylist = new List<block>(printlist);

            merge_by_name();
            printlist.Clear();
            printlist = new List<block>(mylist);

        }


        /*
         * helper functions
         */
        private void input(List<startlist> listOfstartlist)
        {
            // memorysize = getmemorysize(listOfstartlist);//get the size of the memory
            // mylist.Add(new block(0, memorysize, 1));        //assume all the memory is full and allocate its space
            get_the_memory_ready(listOfstartlist);
            merge();
            // print();

        }
        private void merge()
        {
            block element1, element2;
            for (int i = 1; i < mylist.Count; i++)
            {
                if (mylist[i - 1].type == mylist[i].type)
                {
                    element1 = mylist[i - 1];
                    element2 = mylist[i];
                    mylist.RemoveAt(i - 1);
                    mylist.RemoveAt(i - 1);
                    element1.endingaddress = element2.endingaddress;
                    mylist.Insert(i - 1, element1);
                    i--;
                }
            }
            //block size
            calcblocksizeandname();
            //turn back to list
        }

        private void merge_by_name()
        {
            block element1, element2;
            for (int i = 1; i < mylist.Count; i++)
            {
                if (mylist[i - 1].name == mylist[i].name)
                {
                    element1 = mylist[i - 1];
                    element2 = mylist[i];
                    mylist.RemoveAt(i - 1);
                    mylist.RemoveAt(i - 1);

                    element1.endingaddress = element2.endingaddress;
                    element1.blocksize += element2.blocksize;

                    mylist.Insert(i - 1, element1);
                    i--;
                }
            }
        }



        private void calcblocksizeandname()
        {
            int count = mylist.Count;
            block[] data = new block[count];
            mylist.CopyTo(data);
            for (int i = 0; i < count; i++)
            {
                if (data[i].type == 0)
                    data[i].name = "HOLE";
                if (data[i].type == 1)
                    data[i].name = "BUSY";
                data[i].blocksize = data[i].endingaddress - data[i].startingaddress;
            }
            mylist.Clear();
            for (int i = 0; i < count; i++)
            {
                mylist.Add(data[i]);
            }
        }
        private int getmemorysize(List<startlist> listOfstartlist)
        {
            int memorysize = 0;
            int local;
            foreach (var k in listOfstartlist)
            {
                local = k.startingaddress + k.space;
                if (local > memorysize)
                    memorysize = local;
            }
            return memorysize;
        }
        private void get_the_memory_ready(List<startlist> listOfstartlist)
        {
            //List<startlist>.Sort<startlist>(startlist,  (x ,  y) => x.startingaddress.CompareTo(y.startingaddress));

            listOfstartlist.Sort((x, y) => x.startingaddress.CompareTo(y.startingaddress));

            /*
            Console.WriteLine("input sorted is/************");
            foreach (var e in listOfstartlist)
                Console.WriteLine(e.startingaddress + "  " + e.space + "  ");
            Console.WriteLine("/**********************************");
            */

            int timer = 0;
            block local;
            local.blocksize = 0;
            local.name = "BUSY";
            foreach (var e in listOfstartlist)
            {
                if (timer == e.startingaddress)
                {

                    local.startingaddress = e.startingaddress;
                    local.endingaddress = e.startingaddress + e.space;
                    local.type = 0;
                    mylist.Add(local);
                    timer = local.endingaddress;

                }
                else if (timer > e.startingaddress)
                {

                    local.startingaddress = timer;
                    local.endingaddress = e.startingaddress + e.space;
                    local.type = 0;
                    mylist.Add(local);
                    timer = local.endingaddress;

                }

                else
                {

                    local.startingaddress = timer;
                    local.endingaddress = e.startingaddress;
                    local.type = 1;
                    mylist.Add(local);

                    local.startingaddress = e.startingaddress;
                    local.endingaddress = e.startingaddress + e.space;
                    local.type = 0;
                    mylist.Add(local);
                    timer = local.endingaddress;


                }
            }



            if (timer != memorysize)
            {

                local.startingaddress = timer;
                local.endingaddress = memorysize;
                local.type = 1;
                mylist.Add(local);

            }


            //print();

        }
        private void print()
        {
            Console.WriteLine(" mylist  is/************/");
            foreach (var e in mylist)
            {
                Console.WriteLine(e.startingaddress + "  " + e.endingaddress + "  " + e.type + " " + e.name + " " + e.blocksize);
            }
            Console.WriteLine("/***********************************/");



        }
    }



}
