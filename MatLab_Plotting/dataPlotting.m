MyFolderInfo = dir('../DataLogging/VelocityJoint/*.csv')

for file = 1:size(MyFolderInfo,1)

    fileName = MyFolderInfo(file).name

    T = readtable(sprintf('../DataLogging/VelocityJoint/%s', fileName),'NumHeaderLines',1)
    
    Data = table2array(T);
    x = Data(:,1);
    Time = Data(:,43);
    
    
    filterX = zeros(29,1)
    filterT = zeros(29,1)
    
    %figure(1)
    fh = figure('Name', sprintf('Right Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 1:21
        x = Data(:,joint);
        it = 1
        for i = 1:size(x,1)
            if x(i,1) ~= 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end
      
        nexttile
        plot(filterT,filterX,'-o')
        ylim([0 100])
        title(sprintf('Plot of the variance of mean Velocity in the interval from last Timestamp represented to this one of the Joint%i of the Right Hand', joint-1))
        ylabel('Mean Velocity') 
        xlabel('Final Timestamp in Interval from last Timestamp represented to this one') 
        legend({'Mean Velocity'},'Location','northwest')
        ax = gca;
        ax.FontSize = 5;
    end
    
    %figure(2)
    fh = figure('Name', sprintf('Left Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 22:42
        x = Data(:,joint);
        it = 1
        for i = 1:size(x,1)
            if x(i,1) ~= 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end
        
        nexttile
        plot(filterT,filterX,'-o')
        ylim([0 100])
        title(sprintf('Plot of the variance of mean Velocity in the interval from last Timestamp represented to this one of the Joint%i of the Left Hand', joint-22))
        ylabel('Mean Velocity') 
        xlabel('Final Timestamp in Interval from last Timestamp represented to this one') 
        legend({'Mean Velocity'},'Location','northwest')
        ax = gca;
        ax.FontSize = 5;
    end
end